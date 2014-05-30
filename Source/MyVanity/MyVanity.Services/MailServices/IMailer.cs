using System.Net.Mail;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.MailServices
{
    /// <summary>
    /// Represents a mailer service
    /// </summary>
    public interface IMailer : IPerRequestDependency
    {
        bool SendMail(MailMessage email);
    }
}
