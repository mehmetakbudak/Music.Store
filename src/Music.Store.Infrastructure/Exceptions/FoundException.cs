using Music.Store.Domain.Models;
using System.Net;

namespace Music.Store.Infrastructure.Exceptions
{
    public class FoundException : ApiExceptionBase
    {
        public FoundException() : base()
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.Found, Message = "Kayıt Mevcut." };
        }

        public FoundException(string message) : base(message)
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.Found, Message = message };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.Found;
    }
}
