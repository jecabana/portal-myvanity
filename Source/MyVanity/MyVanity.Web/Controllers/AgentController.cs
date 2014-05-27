using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using MyVanity.Common;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Services.Blobs;
using MyVanity.Services.MailServices;
using MyVanity.Services.Membership;
using MyVanity.Views.Filters;
using MyVanity.Views.Repositories.AgentViewsRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AgentController : FileManagingController
    {
        private readonly IAgentViewRepository _agentViewRepository;
        private readonly IModelConverter<Agent, AgentEditModel> _agentModelConverter;
        private readonly IMembershipService _membershipService;
        private readonly IMessageCenter _mailService;
        private readonly IUnitOfWork _unitOfWork;

        public AgentController(IAgentViewRepository agentViewRepository, IMembershipService membershipService, IModelConverter<Agent, AgentEditModel> agentModelConverter, 
                               IMessageCenter mailService, IBlobStore blobStore, IUnitOfWork unitOfWork) : base(membershipService, blobStore)
        {
            _membershipService = membershipService;
            _agentModelConverter = agentModelConverter;
            _mailService = mailService;
            _unitOfWork = unitOfWork;
            _agentViewRepository = agentViewRepository;
        }

        public ActionResult Index(FilterInformation filterInfo)
        {
            var agentsRepository = _agentViewRepository.Get(filterInfo);
            var model = new AgentIndexModel(agentsRepository);

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new AgentEditModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(AgentEditModel model)
        {
            if (ModelState.IsValid)
            {
                var agent = _agentModelConverter.ConvertToSource(model);
                var pwd = Membership.GeneratePassword(8, 2);
                var roles = new List<ApplicationRole>
                            {
                                _agentViewRepository.MapTypeToRole(model.Type),
                                ApplicationRole.Agent
                            };
                var result = await _membershipService.CreateAsync(agent,roles, pwd);

                if (result.Succeeded)
                {
                    _mailService.SendEmailMessage("AgentCreated", new
                    {
                        Name = model.ProfileDetails.FullName,
                        model.UserName,
                        Password = pwd,
                    }, model.Email, Constants.VanityMail, "Welcome to MyVanity", null);


                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _agentViewRepository.FindById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AgentEditModel model)
        {
            if (ModelState.IsValid)
            {
                _agentViewRepository.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<JsonResult> Delete(int agentId, string userName)
        {
            var repository = _unitOfWork.GetRepository<Agent>();
            var agent = repository.FindById(agentId);

            var deleteErrors = new List<string>();
            
            if (agent.Patients != null && agent.Patients.Count != 0)
                deleteErrors.Add("This agent's patients must be reassinged first");
            
            if (agent.UserProcedures != null && agent.UserProcedures.Count != 0)
                deleteErrors.Add("This agent's procedures must be reassigned first");

            if (!deleteErrors.Any())
            {
                await _membershipService.RemoveAsync<Agent>(userName);
                
                return Json(new
                           {
                                success = true,
                                message = string.Empty
                           });
            }

            return Json(new
                        {
                            success = false,
                            message = string.Join("\n", deleteErrors)
                        });
        }
    }
}