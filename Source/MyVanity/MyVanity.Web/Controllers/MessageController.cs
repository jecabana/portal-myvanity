using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MyVanity.Model.MessageModels;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;
using MyVanity.Services.Membership;
using MyVanity.Services.MessageServices;
using MyVanity.Views.Filters;
using MyVanity.Views.Repositories.MessageViewRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles="Agent, Patient")]
    public class MessageController : BaseController
    {
        private readonly IMessageViewRepository _viewRepository;
        private readonly IMessageService _messageService;

        public MessageController(IMembershipService membershipService,
                                 IMessageViewRepository viewRepository, 
                                 IMessageService messageService) : base(membershipService)
        {
            _viewRepository = viewRepository;
            _messageService = messageService;
        }

        public ActionResult Inbox(FilterInformation info)
        {
            var pagedResult = GetInboxMessages(info);
            return View(new MessageIndexModel(pagedResult));
        }

        [Authorize(Roles = "Patient")]
        public PartialViewResult PartialInbox(FilterInformation info)
        {
            var pagedResult = GetInboxMessages(info);
            
            return PartialView("Partials/_PartialInboxMessageList", new PagedViewModel<MessageEditModel>
                                                            {
                                                                Items = pagedResult.Result,
                                                                Page = pagedResult.PageNumber,
                                                                PageSize = pagedResult.PageSize,
                                                                TotalPages = pagedResult.TotalPages,
                                                                TotalRecords = pagedResult.TotalItems,
                                                                SortMode = pagedResult.SortOrder,
                                                                OrderColumn = pagedResult.SortColumn
                                                            });
        }

        private PagedResult<IEnumerable<MessageEditModel>> GetInboxMessages(FilterInformation info)
        {
            var loggedUserId = CurrentUser.OwnerId;
            return _viewRepository.GetInboxMessagesForUser(loggedUserId, info);    
        }

        public ActionResult Sent(FilterInformation info)
        {
            var pagedResult = GetOutBoxMessages(info);
            var model = new MessageIndexModel(pagedResult);

            return View(model);
        }

        public PartialViewResult PartialSent(FilterInformation info)
        {
            var pagedResult = GetOutBoxMessages(info);
            return PartialView("Partials/_PartialOutboxMessageList", new PagedViewModel<MessageEditModel>
                                                                     {
                                                                         Items = pagedResult.Result,
                                                                         Page = pagedResult.PageNumber,
                                                                         PageSize = pagedResult.PageSize,
                                                                         TotalPages = pagedResult.TotalPages,
                                                                         TotalRecords = pagedResult.TotalItems,
                                                                         SortMode = pagedResult.SortOrder,
                                                                         OrderColumn = pagedResult.SortColumn
                                                                     });
        }

        private PagedResult<IEnumerable<MessageEditModel>> GetOutBoxMessages(FilterInformation info)
        {
            var loggedUserId = CurrentUser.OwnerId;
            return _viewRepository.GetOutboxMessagesForUser(loggedUserId, info);
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