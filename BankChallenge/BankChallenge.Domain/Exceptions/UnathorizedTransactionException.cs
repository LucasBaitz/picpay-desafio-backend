using System.Net;
using BankChallenge.Domain.Exceptions.Base;

namespace BankChallenge.Domain.Exceptions
{
    public class UnathorizedTransactionException : BaseDomainException
	{
		public UnathorizedTransactionException(string message, string title) : base(message, title)
		{
			StatusCode = HttpStatusCode.BadRequest;
		}
	}
}
