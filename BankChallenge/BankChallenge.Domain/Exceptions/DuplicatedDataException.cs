using System.Net;
using BankChallenge.Domain.Exceptions.Base;

namespace BankChallenge.Domain.Exceptions
{
    public class DuplicatedDataException : BaseDomainException
	{
		public DuplicatedDataException(string message, string title) : base(message, title)
		{
			StatusCode = HttpStatusCode.BadRequest;
		}
	}
}
