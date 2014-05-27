using System;
using System.Linq;
using System.Web.Mvc;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.MessageModels;
using MyVanity.Services.Membership;
using MyVanity.Services.MessageServices;
using MyVanity.Views.Repositories.MessageViewRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles="Agent, Patient")]
    public class MessageController : BaseController
    {
        private readonly IMessageViewRepository _viewRepository;
        private readonly IModelConverter<Message, MessageEditModel> _modelConverter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageService _messageService;

        public MessageController(IMembershipService membershipService,
                                 IMessageViewRepository viewRepository, 
                                 IModelConverter<Message, MessageEditModel> modelConverter, 
                                 IUnitOfWork unitOfWork, IMessageService messageService) : base(membershipService)
        {
            _viewRepository = viewRepository;
            _modelConverter = modelConverter;
            _unitOfWork = unitOfWork;
            _messageService = messageService;
        }

        public ActionResult Inbox()
        {
            var loggedUserId = CurrentUser.OwnerId;
            var user = _unitOfWork.GetRepository<User>().FindById(loggedUserId);

            var inboxMessages = user.Inbox.Select(x => _modelConverter.ConvertToModel(x));
            var model = new MessageIndexModel(inboxMessages);

            return View(model);
        }

        public ActionResult Sent()
        {
            var loggedUserId = CurrentUser.OwnerId;
            var user = _unitOfWork.GetRepository<User>().FindById(loggedUserId);

            var sentMessages = user.Outbox.Select(x => _modelConverter.ConvertToModel(x));
            var model = new MessageIndexModel(sentMessages);

            return View(model);
        }

        public ActionResult Compose()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Compose(MessageEditModel model)
        {
            if (ModelState.IsValid)
            {
                model.FromUserId = CurrentUser.OwnerId;

                _viewRepository.Insert(model);
                return RedirectToAction("Inbox");
            }

            return View(model);
        }

        public JsonResult MessageTo(int toId, int fromId, string body, int? repliesTo)
        {
            try
            {
                var model = new MessageEditModel();

                if (repliesTo.HasValue && repliesTo.Value != 0)
                    model = _viewRepository.GetModelForReplyingTo(repliesTo.Value);
                else
                {
                    model.ToUserId = toId;
                    model.Subject = "New Message!";
                }

                model.FromUserId = fromId;
                model.Body = body;
                model.Date = DateTime.Now;

                _viewRepository.Insert(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new {success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            _viewRepository.Delete(id);
            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public JsonResult SetMessageRead(int messageId)
        {
            try
            {
                _messageService.SetMessageRead(messageId);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public ActionResult ReplyTo(int id)
        {
            var message = _viewRepository.GetModelForReplyingTo(id);
            return View("Compose", message);
        }
    }
}