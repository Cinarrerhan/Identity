using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace IdentityManager.Service
{
    public class EmailJetEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public MailJetOptions _mailJetOptions;

        public EmailJetEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _mailJetOptions = _configuration.GetSection("MailJet").Get<MailJetOptions>();
            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "cinarerhan@protonmail.com")

                .Property(Send.FromName, "Erhan")

                .Property(Send.Subject, subject)

                .Property(Send.HtmlPart, htmlMessage)

                .Property(Send.Recipients, new JArray {

                    new JObject {

                        {"Email", email}

                    }

                });

            await client.PostAsync(request);
        }
    }
}



