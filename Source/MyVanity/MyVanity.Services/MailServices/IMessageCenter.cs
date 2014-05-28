using System.Collections.Generic;
using System.Net.Mail;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.MailServices
{
    public interface IMessageCenter : IPerRequestDependency
    {
        bool SendEmailMessage(string template, object viewModel, string to, string from, string subject, params string[] replyToAddresses);

		bool SendEmailMessageWithAttachments(string template, object viewModel, string to, string from, string subject, List<Attachment> attachedFiles, params string[] replyToAddresses);
    }
}
