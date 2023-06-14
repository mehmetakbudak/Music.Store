using Music.Store.Domain.Enums;

namespace Music.Store.Domain.Entities
{
    public class MailTemplate : EntityBase
    {
        public TemplateType TemplateType { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
