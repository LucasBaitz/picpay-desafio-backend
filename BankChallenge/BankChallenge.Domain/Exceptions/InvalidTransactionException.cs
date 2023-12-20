using System.Net;
using BankChallenge.Domain.Exceptions.Base;

namespace BankChallenge.Domain.Exceptions
{
    public class InvalidTransactionException : BaseDomainException
	{
		public InvalidTransactionException(string message, string title) : base(message, title)
		{
			StatusCode = HttpStatusCode.BadRequest;
		}
	}
}
