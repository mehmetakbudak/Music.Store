﻿using Music.Store.Domain.Consts;
using Music.Store.Domain.Models;
using Music.Store.Domain.Models.MailTemplate;
using Music.Store.Domain.Models.WebsiteParameter;
using Music.Store.Service.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Music.Store.Infrastructure.Helpers
{
    public interface IMailService
    {
        Task<ServiceResult> Send(MailModel model);

        Task<ServiceResult> SendWithTemplate(MailWithTemplateModel model);
    }

    public class MailService : IMailService
    {
        private readonly IWebsiteParameterService _websiteParameterService;
        private readonly IMailTemplateService _mailTemplateService;

        public MailService(IWebsiteParameterService websiteParameterService,
            IMailTemplateService mailTemplateService)
        {
            _websiteParameterService = websiteParameterService;
            _mailTemplateService = mailTemplateService;
        }

        public async Task<ServiceResult> Send(MailModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };
            try
            {
                var emailSetting = await _websiteParameterService.GetParametersByType<EmailSettingModel>(WebsiteParameterTypes.EmailSettings);

                Int32.TryParse(emailSetting.Port, out int port);

                using (var client = new SmtpClient(emailSetting.Host, port))
                {
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(emailSetting.EmailAddress, emailSetting.Password);

                    MailMessage message = new MailMessage();

                    message.From = new MailAddress(emailSetting.EmailAddress);
                    message.To.Add(model.EmailAddress);
                    message.Body = model.Body;
                    message.Subject = model.Subject;
                    message.IsBodyHtml = true;
                    await Task.Run(() => client.Send(message));
                }
            }
            catch
            {
                result.HttpStatusCode = HttpStatusCode.InternalServerError;
                result.Message = "Mail gönderilirken hata oluştu.";
            }
            return result;
        }

        public async Task<ServiceResult> SendWithTemplate(MailWithTemplateModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };
            try
            {
                var emailSetting = await _websiteParameterService.GetParametersByType<EmailSettingModel>(WebsiteParameterTypes.EmailSettings);

                var data = model.Data;

                var mailTemplate = await _mailTemplateService.GetTemplateByType(data, model.TemplateType);

                if (mailTemplate != null)
                {
                    if (string.IsNullOrEmpty(model.Subject))
                    {
                        model.Subject = mailTemplate.Subject;
                    }
                    if (string.IsNullOrEmpty(model.Body))
                    {
                        model.Body = mailTemplate.Body;
                    }
                }

                Int32.TryParse(emailSetting.Port, out int port);

                using (var client = new SmtpClient(emailSetting.Host, port))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(emailSetting.EmailAddress, emailSetting.Password);

                    MailMessage message = new MailMessage();

                    message.From = new MailAddress(emailSetting.EmailAddress);
                    message.To.Add(model.EmailAddress);
                    message.Body = model.Body;
                    message.Subject = model.Subject;
                    message.IsBodyHtml = true;
                    await Task.Run(() => client.Send(message));
                }
            }
            catch
            {
                result.HttpStatusCode = HttpStatusCode.InternalServerError;
                result.Message = "Mail gönderilirken hata oluştu.";
            }
            return result;
        }
    }
}
