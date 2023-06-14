namespace Music.Store.Domain.Entities
{
    public class AccessRightEndpoint : EntityBase
    {
        public int AccessRightId { get; set; }

        public AccessRight AccessRight { get; set; }

        public int RouteLevel { get; set; }

        public string Endpoint { get; set; }

        public string Method { get; set; }
    }
}
