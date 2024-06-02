using UserCRUD.Application.Models.Common.Email;

namespace UserCRUD.Application.Contracts.Services;

public interface IEmailService
{
    Task<EmailSendResponseDto> Send(EmailSendDto mail);
}
