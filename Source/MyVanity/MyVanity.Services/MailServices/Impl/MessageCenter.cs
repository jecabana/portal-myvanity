using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;
using RazorEngine;
using RazorEngine.Templating;

namespace MyVanity.Services.MailServices.Impl
{
    public class MessageCenter : IMessageCenter
    {        
        private readonly IMailer _mailer;

        public MessageCenter(IMailer mailer)
        {
            _mailer = mailer;
        }

        public bool SendEmailMessage(string template, object viewModel, string to, string @from, string subject, params string[] replyToAddresses)
        {
            var compiledTemplate = LoadTemplate(template, viewModel);
            return SendEmail(from, to, subject, compiledTemplate, from, null, replyToAddresses);
        }

        public bool SendEmailMessageWithAttachments(string template, object viewModel, string to, string @from, string subject, List<Attachment> attachedFiles, params string[] replyToAddresses)
        {
            var compiledTemplate = LoadTemplate(template, viewModel);
            return SendEmail(from, to, subject, compiledTemplate, from, attachedFiles, replyToAddresses);
        } 

        private bool SendEmail(string from, string to, string subject, string body, string replyTo, List<Attachment> attachedFiles, params string[] replyToAddresses)
        {
            replyTo = replyTo ?? from;
            attachedFiles = attachedFiles ?? new List<Attachment>();

            var message = new MailMessage(@from, to, subject, body);
            message.ReplyToList.Add(replyTo);

            foreach (var attachedFile in attachedFiles)
                message.Attachments.Add(attachedFile);

            return _mailer.SendMail(message);
        }

        public string LoadTemplate(string template, object viewModel)
        {
            var templateContent = AttemptLoadEmailTemplate(template);


            //Configure RazorEngine
            var tplConfig = new RazorEngine.Configuration.TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(MailTemplate<>)
            };

            using (var service = new TemplateService(tplConfig))
            {
                Razor.SetTemplateService(service);

                var compiledTemplate = Razor.Parse(templateContent, viewModel);
                return compiledTemplate;
            }
        }

        private string AttemptLoadEmailTemplate(string name)
        {
            if (File.Exists(name))
            {
                var templateText = File.ReadAllText(name);
                return templateText;
            }

            var template = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/EmailTemplates/{0}.html", name));

            if (File.Exists(template))
            {
                var templateText = File.ReadAllText(template);
                return templateText;
            }

            return null;
        }
    }
}
