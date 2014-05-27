using MyVanity.Model.MessageModels;

namespace MyVanity.Views.Repositories.MessageViewRepository
{
    public interface IMessageViewRepository : IViewRepository<MessageEditModel>
    {
        MessageEditModel GetModelForReplyingTo(int replyTo);
    }
}
