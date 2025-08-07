using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace WebAppBackend.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SmtpEmailSender> _logger;
        private readonly bool _isProduction;
        private readonly bool _isStaging;


        public SmtpEmailSender(IConfiguration config, ILogger<SmtpEmailSender> logger)
        {
            _config = config;
            _logger = logger;
            if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Production")
            {
                _isProduction = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Production";
            }
            else
            {
                _isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
            }

            if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Staging")
            {
                _isStaging = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Staging";
            }
            else
            {
                _isStaging = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Staging";
            }

            _logger = logger;

        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("✅ SmtpEmailSender is being used!");
            try
            {

                string host = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Host") : _config["Smtp:Host"];
                int port = int.Parse(_isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Port") : _config["Smtp:Port"]);
                string username = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Username") : _config["Smtp:Username"];
                string password = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Password") : _config["Smtp:Password"];
                string from = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__From") : _config["Smtp:From"];

                _logger.LogInformation("SMTP Config: Host={Host}, Port={Port}, Username={Username}, From={From}", host, port, username, from);

                if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(from))
                {
                    throw new InvalidOperationException("One or more SMTP configuration values are missing or empty.");
                }


                using var smtpClient = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                _logger.LogInformation("Sending email to {Email} with subject '{Subject}'", email, subject);
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully!");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to send email.");
                throw;
            }
        }
    }
}