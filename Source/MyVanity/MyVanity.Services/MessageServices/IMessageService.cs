using MyVanity.Common.Autofac;

namespace MyVanity.Services.MessageServices
{
    public interface IMessageService : IPerRequestDependency
    {
        void SetMessageRead(int messageId);
    }
}
