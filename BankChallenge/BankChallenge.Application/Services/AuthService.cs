using AutoMapper;
using BankChallenge.Application.DTOs.UserDTOs;
using BankChallenge.Application.Services.Interfaces;
using BankChallenge.Domain.Exceptions;
using BankChallenge.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BankChallenge.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ITokenGeneratorService _tokenService;
		private readonly IMapper _mapper;
		private readonly ILogger<AuthService> _logger;
		public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenGeneratorService tokenService, ILogger<AuthService> logger)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_tokenService = tokenService;
			_logger = logger;
		}
		public async Task Register(UserCreateAccountDTO createUser)
		{
			CheckIfInformationIsUnique(createUser.Document, createUser.Email);

			var newUser = _mapper.Map<User>(createUser);
			newUser.UserName = newUser.Email;

			var createResult = await _userManager.CreateAsync(newUser, createUser.Password);

			if (!createResult.Succeeded)
			{
				foreach (var item in createResult.Errors)
				{
					_logger.LogError(item.Description);
				}
			}
		}

		public async Task<string> Login(UserLoginDTO loginUser)
		{
			var userLoginAttempt = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);

			if (!userLoginAttempt.Succeeded) throw new ApplicationException("User not authenticated");

			var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedEmail == loginUser.Email.ToUpper()) ?? throw new Exception();

			var token = _tokenService.GenerateToken(user);

			return token;
		}
		public async Task DeleteUser(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			await _userManager.DeleteAsync(user);
		}

		public async Task UpdateUser(string userId, UserUpdateInformationDTO updateDto)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user is null) throw new ResourceNotFoundException("Resource not found", "User was not found");

			CheckIfInformationIsUnique(updateDto.Document, updateDto.Email);
		
			user.Email = updateDto.Email;
			user.UserName = updateDto.Email;
			user.Document = updateDto.Document;
			user.FirstName = updateDto.FirstName;
			user.LastName = updateDto.LastName;
			user.PhoneNumber = updateDto.PhoneNumber;


			await _userManager.UpdateAsync(user);
		}

		public async Task<User?> GetUserByDocument(string document)
		{
			return await _userManager.Users.FirstOrDefaultAsync(u => u.Document == document);
		}

		public async Task<User?> GetUserById(Guid id)
		{
			return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _userManager.Users.ToListAsync();
		}

		public void CheckIfInformationIsUnique(string document, string email)
		{
			if (_userManager.Users.Any(u => u.Document == document))
				throw new DuplicatedDataException("The provided document must be unique.", "Invalid Data");

			if (_userManager.Users.Any(u => u.Email == email))
				throw new DuplicatedDataException("The provided email is already being used", "Invalid Data");
		}

	}
}
