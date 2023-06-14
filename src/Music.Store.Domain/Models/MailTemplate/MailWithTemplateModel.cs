using Music.Store.Domain.Enums;

namespace Music.Store.Domain.Models.MailTemplate
{
    public class MailWithTemplateModel : MailModel
    {
        public TemplateType TemplateType { get; set; }

        public object Data { get; set; }
    }
}
