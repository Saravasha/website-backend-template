using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace GoshehArtWebApp.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly bool _isProduction;
        private readonly bool _isStaging;
    

       public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
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
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
               
                string host = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Host") : _config["Smtp:Host"];
                int port = int.Parse(_isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Port") : _config["Smtp:Port"]);
                string username = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Username") : _config["Smtp:Username"];
                string password = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__Password") : _config["Smtp:Password"];
                string from = _isProduction || _isStaging ? Environment.GetEnvironmentVariable("Smtp__From") : _config["Smtp:From"];

                var smtpClient = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);
                await smtpClient.SendMailAsync(mailMessage);
                
            
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }
}