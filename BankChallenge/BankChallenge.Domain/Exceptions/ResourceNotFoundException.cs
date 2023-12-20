using System.Net;
using BankChallenge.Domain.Exceptions.Base;

namespace BankChallenge.Domain.Exceptions
{
    public class ResourceNotFoundException : BaseDomainException
	{
		public ResourceNotFoundException(string message, string title) : base(message, title)
		{
			StatusCode = HttpStatusCode.NotFound;
		}
	}
}