namespace Music.Store.Domain.Entities
{
    public class WebsiteParameter : EntityBase
    {
        public int? ParentId { get; set; }

        public string Code { get; set; }

        public string Value { get; set; }

        public bool Required { get; set; }

        public bool Visible { get; set; }
    }
}
