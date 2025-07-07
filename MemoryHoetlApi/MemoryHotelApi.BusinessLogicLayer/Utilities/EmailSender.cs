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

        public async Task SendEmailCreateAdminAsync(string toEmail, string password)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Memory Hotel: You are created an account by Memory Hotel admin!";

            var bodyBuilder = new BodyBuilder
            {
                // Notify the user that the account is created by admin with the password
                HtmlBody = $@"
                    <h2>Memory Hotel - Account Created</h2>
                    <p>Dear Customer,</p>
                    <p>Your account has been created by the Memory Hotel admin. Please find your login credentials below:</p>
                    <p><strong>Email:</strong> {toEmail}</p>
                    <p><strong>Password:</strong> {password}</p>
                    <p>We recommend changing your password after your first login.</p>
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

        public async Task SendEmailForBlogNotification(List<string> toEmails, string blogTitle, string bogSlugAndId)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            foreach (var email in toEmails)
            {
                message.To.Add(new MailboxAddress("", email));
            }
            message.Subject = "Memory Hotel: New Blog Post Notification";
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <h2>New Blog Post Alert!</h2>
                    <p>Dear Customer,</p>
                    <p>We are excited to announce a new blog post titled <strong>{blogTitle}</strong> has been published on our Memory Hotel blog.</p>
                    <p>You can read the full article by clicking the link below:</p>
                    <p><a href='https://www.vietnammemorytravel.com/blogs/{bogSlugAndId}'>Read the Blog Post</a></p>
                    <p>Thank you for being a part of our community!</p>
                    <p>Best regards,<br>The Memory Hotel Team"
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
