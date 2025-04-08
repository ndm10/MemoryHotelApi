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

        public async Task SendOtpEmailAsync(string toEmail, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Memory Hotel: Your OTP Code";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <h2>Memory Hotel - Your OTP Code</h2>
                    <p>Hello,</p>
                    <p>Your one-time password (OTP) for account verification is: <strong>{otp}</strong></p>
                    <p>This code is valid for 1 minutes. If you didn’t request this, please ignore this email or contact support.</p>
                    <p>Best regards,<br>Memory Hotel Team</p>"
            };
            message.Body = bodyBuilder.ToMessageBody();

            // Gửi email qua SMTP
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
