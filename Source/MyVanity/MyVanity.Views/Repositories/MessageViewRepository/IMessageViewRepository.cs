using System.Collections.Generic;
using MyVanity.Model.MessageModels;
using MyVanity.Model.Results;
using MyVanity.Views.Filters;

namespace MyVanity.Views.Repositories.MessageViewRepository
{
    public interface IMessageViewRepository : IViewRepository<MessageEditModel>
    {
        MessageEditModel GetModelForReplyingTo(int replyTo);

        PagedResult<IEnumerable<MessageEditModel>> GetInboxMessagesForUser(int userId, FilterInformation info);

        PagedResult<IEnumerable<MessageEditModel>> GetOutboxMessagesForUser(int userId, FilterInformation info);
    }
}
