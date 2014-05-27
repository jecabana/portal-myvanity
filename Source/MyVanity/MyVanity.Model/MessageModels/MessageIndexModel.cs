using System.Collections.Generic;

namespace MyVanity.Model.MessageModels
{
    public class MessageIndexModel
    {
        public MessageIndexModel(IEnumerable<MessageEditModel> messages)
        {
            Messages = messages;
        }

        public IEnumerable<MessageEditModel> Messages { get; set; }
    }
}
