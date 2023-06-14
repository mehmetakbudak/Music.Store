using Music.Store.Domain.Models;
using System.Net;

namespace Music.Store.Infrastructure.Exceptions
{
    public class UnAuthorizedException : ApiExceptionBase
    {
        public UnAuthorizedException() : base()
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.Unauthorized, Message = "Kimlik bilgileri doğrulanamadı." };
        }

        public UnAuthorizedException(string message) : base(message)
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.Unauthorized, Message = message };
        }


        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.Unauthorized;
    }
}
