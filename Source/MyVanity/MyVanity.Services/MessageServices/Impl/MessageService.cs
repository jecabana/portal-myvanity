using MyVanity.Domain;
using MyVanity.Domain.UoW;

namespace MyVanity.Services.MessageServices.Impl
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetMessageRead(int messageId)
        {
            var repository = _unitOfWork.GetRepository<Message>();
            var message = repository.FindById(messageId);
            message.IsRead = true;
            _unitOfWork.SaveChanges();
        }
    }
}
