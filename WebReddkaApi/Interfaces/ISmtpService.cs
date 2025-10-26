using WebReddkaApi.Models.SMTP;

namespace WebReddkaApi.Interfaces;

public interface ISmtpService
{
    Task<bool> SendEmailAsync(EmailMessage message);
}