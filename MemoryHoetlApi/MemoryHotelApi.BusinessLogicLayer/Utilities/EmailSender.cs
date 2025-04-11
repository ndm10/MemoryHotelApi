using MailKit.Net.Smtp;
using MemoryHotelApi.BusinessLogicLayer.Common;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MemoryHotelApi.BusinessLogicLayer.Utilities
{
    public class EmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendOtpRegisterAsync(string toEmail, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Memory Hotel: Your Registration OTP Code";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <h2>Welcome to Memory Hotel!</h2>
                    <p>Dear Customer,</p>
                    <p>Thank you for choosing Memory Hotel. To complete your registration, please use the one-time password (OTP) below:</p>
                    <p style=""font-size: 18px; font-weight: bold; color: #2c3e50;"">{otp}</p>
                    <p>This OTP is valid for <strong>5 minutes</strong>. Please enter it on the registration page to verify your account.</p>
                    <p>We’re excited to have you with us!</p>
                    <p>Best regards,<br>The Memory Hotel Team</p>"
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendOtpResetPasswordAsync(string toEmail, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Memory Hotel: Reset Your Password";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                        <h2>Memory Hotel - Password Reset Request</h2>
                        <p>Dear Customer,</p>
                        <p>We received a request to reset your password for your Memory Hotel account. Please use the one-time password (OTP) below to proceed:</p>
                        <p style=""font-size: 18px; font-weight: bold; color: #2c3e50;"">{otp}</p>
                        <p>This OTP is valid for <strong>5 minutes</strong>. Enter it on the password reset page to create a new password.</p>
                        <p>Stay secure!</p>
                        <p>Best regards,<br>The Memory Hotel Team</p>"
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
