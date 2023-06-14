namespace Music.Store.Domain.Models.AccessRight
{
    public class AccessRightEndpointModel
    {
        public int? Id { get; set; }
        public int RouteLevel { get; set; }
        public string Endpoint { get; set; }
        public string Method { get; set; }
    }
}
