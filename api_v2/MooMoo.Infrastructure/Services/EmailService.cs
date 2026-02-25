using MooMoo.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using MooMoo.Infrastructure.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MooMoo.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly AppSettings _appSettings;
    private readonly EmailSettings _emailSettings;

    public EmailService(
        IOptions<AppSettings> appSettings,
        IOptions<EmailSettings> emailSettings)
    {
        _appSettings = appSettings.Value;
        _emailSettings = emailSettings.Value;
    }

    public async Task SendPasswordResetAsync(string toEmail, string userName, string resetToken, string language = "en")
    {
        var resetUrl = $"{_appSettings.BaseUrl}/reset-password?token={resetToken}";
        
        var (subject, body) = language.ToLower() switch
        {
            "vi" => GetPasswordResetTemplateVi(userName, resetUrl),
            _ => GetPasswordResetTemplateEn(userName, resetUrl)
        };

        await SendEmailAsync(toEmail, subject, body);
    }

    private static (string subject, string body) GetPasswordResetTemplateEn(string userName, string resetUrl)
    {
        var subject = "Reset your password - MooMoo";
        var body = $@"
            <h2>Password Reset Request</h2>
            <p>Hi {userName},</p>
            <p>Click the link below to reset your password:</p>
            <p><a href=""{resetUrl}"" style=""background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; border-radius: 4px; display: inline-block;"">Reset Password</a></p>
            <p>This link will expire in 1 hour.</p>
            <p>If you didn't request this, please ignore this email.</p>
            <p style=""color: #666; font-size: 12px; margin-top: 24px;"">MooMoo - Making family time more rewarding</p>
        ";
        return (subject, body);
    }

    private static (string subject, string body) GetPasswordResetTemplateVi(string userName, string resetUrl)
    {
        var subject = "Đặt lại mật khẩu - MooMoo";
        var body = $@"
            <h2>Yêu cầu đặt lại mật khẩu</h2>
            <p>Xin chào {userName},</p>
            <p>Nhấn vào nút bên dưới để đặt lại mật khẩu:</p>
            <p><a href=""{resetUrl}"" style=""background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; border-radius: 4px; display: inline-block;"">Đặt lại mật khẩu</a></p>
            <p>Link này sẽ hết hạn sau 1 giờ.</p>
            <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
            <p style=""color: #666; font-size: 12px; margin-top: 24px;"">MooMoo - Biến thời gian gia đình thành phần thưởng</p>
        ";
        return (subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var from = new EmailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlBody);
        
        var response = await client.SendEmailAsync(msg);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Body.ReadAsStringAsync();
            throw new InvalidOperationException($"SendGrid failed: {response.StatusCode} - {errorBody}");
        }
    }
}
