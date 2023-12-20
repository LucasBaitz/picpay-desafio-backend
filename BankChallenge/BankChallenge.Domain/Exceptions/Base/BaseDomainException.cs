using System.Net;

namespace BankChallenge.Domain.Exceptions.Base
{
    public class BaseDomainException : Exception
    {
        public string Title { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; } = HttpStatusCode.BadRequest;
        public BaseDomainException(string message, string title) : base(message)
        {
            Title = title;
        }
    }
}
