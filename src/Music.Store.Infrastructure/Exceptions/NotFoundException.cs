using Music.Store.Domain.Models;
using System;
using System.Net;

namespace Music.Store.Infrastructure.Exceptions
{
    public class NotFoundException : ApiExceptionBase
    {
        public NotFoundException() : base()
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.NotFound, Message = "Kayıt bulunamadı." };
        }

        public NotFoundException(String message) : base(message)
        {
            Error = new BaseResult { HttpStatusCode = HttpStatusCode.NotFound, Message = message };
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;
    }
}
