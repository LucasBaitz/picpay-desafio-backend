using BankChallenge.Application.DTOs.UserDTOs;
using BankChallenge.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BankChallenge.API.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IAuthService _authService;
		public UsersController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		[Route("Registration")]
		public async Task<IActionResult> RegisterUser(UserCreateAccountDTO createUser)
		{
			if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated) return BadRequest("You are already logged in. Please log out to register a new account.");

			await _authService.Register(createUser);
			return Ok($"Account created!");
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(UserLoginDTO userLogin)
		{
			if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated)
			{
				return BadRequest("You are already logged in. Please log out to register a new account.");
			}

			var JWTtoken = await _authService.Login(userLogin);
			return Ok(JWTtoken);
		}

		[HttpPut]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Route("UpdateInformation")]
		public async Task<IActionResult> UpdateAccountInformation(UserUpdateInformationDTO updateDto)
		{
			string userId = User.FindFirst("id")!.Value;
			if (string.IsNullOrEmpty(userId))
				return BadRequest();
			
			await _authService.UpdateUser(userId, updateDto);
			return Ok();
			
		}

		[HttpDelete]
		[Route("DeleteAccount")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public async Task<IActionResult> DeleteAccount()
		{
			string userId = User.FindFirst("id")!.Value;
			await _authService.DeleteUser(userId);
			return NoContent();
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			return Ok(await _authService.GetAll());
		}
	}
}
