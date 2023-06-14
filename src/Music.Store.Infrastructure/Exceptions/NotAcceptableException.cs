using Music.Store.Domain.Models;
using System.Net;

namespace Music.Store.Infrastructure.Exceptions
{
    public class NotAcceptableException : ApiExceptionBase
    {
        public NotAcceptableException() : base()
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.NotAcceptable, Message = "Kabul Edilmeyen İstek." };
        }

        public NotAcceptableException(string message) : base(message)
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.NotAcceptable, Message = message };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.NotAcceptable;
    }
}
