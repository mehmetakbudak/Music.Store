using System.Net;

namespace Music.Store.Domain.Models
{
    public class BaseResult
    {
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class ServiceResult : BaseResult
    {
        public int StatusCode
        {
            get
            {
                return (int)HttpStatusCode;
            }
        }

        public object Data { get; set; }
    }
}
