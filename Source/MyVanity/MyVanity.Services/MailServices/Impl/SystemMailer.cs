using System;
using System.Net.Mail;
using MyVanity.Services.SystemConfiguration;

namespace MyVanity.Services.MailServices.Impl
{
    public class SystemMailer : IMailer
    {
        private readonly IConfigurationService _configurationService;

        public SystemMailer(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public bool SendMail(MailMessage email)
        {
            var containsOverride = _configurationService.GetConfigurationString("overrideEmail") != null;
            var shoudOverrideEmail = containsOverride && (_configurationService.GetConfigurationInt("overrideEmail") == 1);

            var emails = new string[] {};
            if (shoudOverrideEmail)
            {
                emails = _configurationService.GetConfigurationArray("OverrideEmailAddress");    
            }

            var smtpClient = new SmtpClient();

            if (shoudOverrideEmail && emails != null)
            {
                email.To.Clear();
                email.Bcc.Clear();
                email.CC.Clear();

                foreach (var mailAddress in emails)
                {
                    email.To.Add(mailAddress);
                }   
            }

            try
            {
                email.IsBodyHtml = true;
                smtpClient.Send(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
