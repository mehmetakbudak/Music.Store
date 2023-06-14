using Music.Store.Domain.Models;
using System.Net;

namespace Music.Store.Infrastructure.Exceptions
{
    public class ForbiddenException : ApiExceptionBase
    {
        public ForbiddenException() : base()
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.Forbidden, Message = "Yetkisiz Giriş." };
        }

        public ForbiddenException(string message) : base(message)
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.Forbidden, Message = message };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;
    }
}
