using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Enums;
using Music.Store.Domain.Models.MailTemplate;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface IMailTemplateService
    {
        Task<TemplateResponseModel> GetTemplateByType<T>(T data, TemplateType type);
    }

    public class MailTemplateService : IMailTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MailTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TemplateResponseModel> GetTemplateByType<T>(T data, TemplateType type)
        {
            TemplateResponseModel result = null;

            var mailTemplate = await _unitOfWork.Repository<MailTemplate>()
                .Get(x => x.TemplateType == type);

            if (mailTemplate != null)
            {
                var properties = data.GetType().GetProperties();

                var body = mailTemplate.Body;

                foreach (var property in properties)
                {
                    body = body.Replace("{{" + property.Name + "}}", property.GetValue(data).ToString());
                }

                result = new TemplateResponseModel()
                {
                    Subject = mailTemplate.Title,
                    Body = body
                };
            }
            return result;
        }
    }
}
