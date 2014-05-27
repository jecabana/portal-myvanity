using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.MessageModels;
using MyVanity.Model.UserModels;
using MyVanity.Services.MailServices;

namespace MyVanity.Views.Repositories.MessageViewRepository.Impl
{
    public class MessageViewRepository : ViewRepository<Message, MessageEditModel>, IMessageViewRepository
    {
        private readonly IMessageCenter _mailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelConverter<User, UserViewModel> _userModelConverter;
        private readonly IModelConverter<Message, MessageEditModel> _modelConverter; 

        public MessageViewRepository(IModelConverter<Message, MessageEditModel> modelConverter, IUnitOfWork unitOfWork, IMessageCenter mailService, 
                                     IModelConverter<User, UserViewModel> userModelConverter)
            : base(modelConverter, unitOfWork)
        {
            _mailService = mailService;
            _userModelConverter = userModelConverter;
            _unitOfWork = unitOfWork;
            _modelConverter = modelConverter;
        }

        public override void Insert(MessageEditModel model)
        {
            try
            {
                base.Insert(model);

                var userRepository = _unitOfWork.GetRepository<User>();
                
                var to = userRepository.FindById(model.ToUserId);
                var from = userRepository.FindById(model.FromUserId);

                var toModel = _userModelConverter.ConvertToModel(to);
                var fromModel = _userModelConverter.ConvertToModel(from);


                _mailService.SendEmailMessage("MessageSent", new
                                                             {
                                                                 Name = string.Format("{0} {1}", toModel.FirstName, toModel.MiddleName),
                                                                 model.Body,
                                                                 Sender = string.Format("{0} {1}", fromModel.FirstName, fromModel.MiddleName),
                                                             }, toModel.Email, fromModel.Email, "New Message");
            }
            catch { }
        }

        public MessageEditModel GetModelForReplyingTo(int replyTo)
        {
            var repository = _unitOfWork.GetRepository<Message>();
            var entity = repository.FindById(replyTo);

            var message = _modelConverter.ConvertToModel(entity);
            message.ToUserId = message.FromUserId;
            message.ToUserName = message.FromUserName;
            message.Body = string.Empty;
            message.Subject = string.Format("Re: {0}", message.Subject);
            message.RepliesTo = replyTo;

            return message;
        }
    }
}
 