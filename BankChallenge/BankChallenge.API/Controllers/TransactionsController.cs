using BankChallenge.Application.DTOs.TransactionDTOs;
using BankChallenge.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankChallenge.API.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class TransactionsController : ControllerBase
	{
		private readonly ITransactionService _transactionService;
		public TransactionsController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpPost]
		[Route("Send")]
		public async Task<IActionResult> MakeTransaction(MakeTransactionDTO transactionInfo)
		{
			var senderId = User.FindFirst("id")!.Value;
			transactionInfo.SenderId = senderId;
			var transactionResult = await _transactionService.MakeTransaction(transactionInfo);

			if (transactionResult) return Ok("Transaction made sucessfully");
			return BadRequest("Something went wrong while trying to make the transaction.");
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTransactionHistory()
		{
			return Ok(await _transactionService.GetTransactions());
		}

		[HttpGet]
		[Route("MyTransactions")]
		public async Task<IActionResult> GetMyMadeTransactions()
		{
			string userId = User.FindFirst("id")!.Value;
			return Ok(await _transactionService.ReadTransactionsByUserId(userId));
		}
	}
}
