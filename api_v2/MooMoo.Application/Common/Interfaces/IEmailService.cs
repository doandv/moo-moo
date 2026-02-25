namespace MooMoo.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetAsync(string toEmail, string userName, string resetToken, string language = "en");
}
