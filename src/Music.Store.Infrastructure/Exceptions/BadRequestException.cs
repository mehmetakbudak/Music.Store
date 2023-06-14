using Music.Store.Domain.Models;
using System.Net;

namespace Music.Store.Infrastructure.Exceptions
{
    public class BadRequestException : ApiExceptionBase
    {
        public BadRequestException() : base()
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.BadRequest, Message = "Geçersiz istek." };
        }

        public BadRequestException(string message) : base(message)
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.BadRequest, Message = message };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
