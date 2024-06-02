using System.Net.Mail;
using System.Net;
using System.Text;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Models.Common.Email;
using UserCRUD.Application.Models.Common.SiteSetting;
using UserCRUD.Domain.Enum;
using Microsoft.Extensions.Options;

namespace UserCRUD.Infrastructure.Implementation.Services;

public class EmailService : IEmailService
{
    private readonly SiteSettings _siteSettings;

    public EmailService(IOptionsSnapshot<SiteSettings> siteSettings)
    {
        _siteSettings = siteSettings.Value;
    }

    public Task<EmailSendResponseDto> Send(EmailSendDto mail)
    {
        var body = new StringBuilder();
        body.AppendFormat(_siteSettings.Smtp.BodyFormat, mail.Body, DateTime.Now.ToLongDateString());

        var message = new MailMessage
        {
            IsBodyHtml = true,
            Subject = mail.Subject,
            Body = body.ToString(),
            From = new MailAddress(_siteSettings.Smtp.FromAddress, _siteSettings.Smtp.FromName),
            To = { new MailAddress(mail.To) }
        };

        if ((bool)mail.Attachments?.Any())
        {
            foreach (var attach in mail.Attachments)
            {
                message.Attachments.Add(new Attachment(attach.StreamContent, attach.Name, attach.ContentType));
            }
        }

        var smtp = new SmtpClient
        {
            Host = _siteSettings.Smtp.Server,
            Credentials = new NetworkCredential(_siteSettings.Smtp.Username, _siteSettings.Smtp.Password),
            Port = _siteSettings.Smtp.Port,
            EnableSsl = true
        };

        smtp.SendAsync(message, userToken: null);

        return Task.FromResult(new EmailSendResponseDto(ResponseCodeEnum.Ok));
    }
}
