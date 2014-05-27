using System;
using MyVanity.Domain;
using MyVanity.Domain.UoW;

namespace MyVanity.Model.MessageModels
{
    public class MessageModelConverter : IModelConverter<Message, MessageEditModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageModelConverter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MessageEditModel ConvertToModel(Message entity)
        {
            return new MessageEditModel
                        {
                            Id = entity.Id,
                            Body = entity.Body,
                            FromUserId = entity.FromId,
                            ToUserId = entity.ToId,
                            FromUserName = GetUserName(entity.From),
                            ToUserName = GetUserName(entity.To),
                            Subject = entity.Subject ?? "No subject",
                            IsRead = entity.IsRead
                        };
        }

        public string GetUserName(User user)
        {
            var fromAsAgent = user as Agent;
            var fromAsPatient = user as Patient;

            if (fromAsAgent != null)
                return fromAsAgent.PersonDetails.FullName;

            return fromAsPatient != null ? fromAsPatient.Profile.FullName : user.UserName;
        }

        public Message ConvertToSource(MessageEditModel model)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var repository = _unitOfWork.GetRepository<Message>();

            var to = userRepository.FindById(model.ToUserId);
            var from = userRepository.FindById(model.FromUserId);
            var message = new Message
                   {
                       Body = model.Body,
                       From = from,
                       To = to,
                       Subject = model.Subject,
                       IsRead = model.IsRead,
                       Date = DateTime.Now
                   };
            
            if (model.RepliesTo.HasValue)
                message.RepliesTo = repository.FindById(model.RepliesTo.Value);

            return message;
        }
    }
}
