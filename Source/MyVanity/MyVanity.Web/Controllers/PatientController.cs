using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MyVanity.Common;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Services.MailServices;
using MyVanity.Services.Membership;
using MyVanity.Views.Filters;
using MyVanity.Views.Repositories.AgentViewsRepository;
using MyVanity.Views.Repositories.PatientViewsRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Agent, Admin")]
    public class PatientController : BaseController
    {
        private readonly IPatientViewRepository _patientViewRepository;
        private readonly IModelConverter<Patient, PatientEditModel> _modelConverter;
        private readonly IMembershipService _membershipService;
        private readonly IMessageCenter _mailService;
        private readonly IAgentViewRepository _agentViewRepository;

        public PatientController(IPatientViewRepository patientViewRepository, IMembershipService membershipService, 
                                 IModelConverter<Patient, PatientEditModel> modelConverter, IMessageCenter mailService, 
                                 IAgentViewRepository agentViewRepository) : base(membershipService)
        {
            _patientViewRepository = patientViewRepository;
            _modelConverter = modelConverter;
            _mailService = mailService;
            _agentViewRepository = agentViewRepository;
            _membershipService = membershipService;
        }

        public async Task<ActionResult> Index(FilterInformation info)
        {
            var isAdmin = await _membershipService.IsInRole(CurrentUser, ApplicationRole.Admin);
            
            var patients = isAdmin ? _patientViewRepository.Get(info) 
                                   : _patientViewRepository.GetPatientsForAgent(CurrentUser.OwnerId, info);
            
            var model = new PatientIndexModel(patients);
            return View(model);
        }

        [Authorize(Roles = "SurgicalCoordinator")]
        public ActionResult Create()
        {
            return View(new PatientEditModel());
        }

        [HttpPost]
        [Authorize(Roles = "SurgicalCoordinator")]
        public async Task<ActionResult> Create(PatientEditModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = _modelConverter.ConvertToSource(model);
                
                var pwd = Membership.GeneratePassword(8, 2);
                var roles = new List<ApplicationRole> {ApplicationRole.Patient};
                var result = await _membershipService.CreateAsync(patient, roles, pwd);

                if (result.Succeeded)
                {
                    _mailService.SendEmailMessage("PatientCreated", new
                                                                    {
                                                                        Name = model.Profile.FullName, 
                                                                        model.UserName, 
                                                                        Password = pwd,
                                                                    }, model.Email, Constants.VanityMail, "Welcome to MyVanity", null);


                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            return View(model);
        }

        [Authorize(Roles = "SurgicalCoordinator")]
        public ActionResult Edit(int id)
        {
            var model = _patientViewRepository.FindById(id); 
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SurgicalCoordinator")]
        public ActionResult Edit(PatientEditModel model)
        {
            if (ModelState.IsValid)
            {
                _patientViewRepository.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "SurgicalCoordinator")]
        public async Task<ActionResult> Delete(string userName)
        {
            await _membershipService.RemoveAsync<Patient>(userName);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SurgicalCoordinator, Admin")]
        public JsonResult ReassignToAgent(int patientId, int agentId)
        {
            try
            {
                _patientViewRepository.ReassignToAgent(patientId, agentId);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public PartialViewResult GetAgentsPartialView(int? distinctFrom, AgentType type = AgentType.SurgicalCoordinator)
        {
            var agents = _agentViewRepository.FilterAgents(distinctFrom, type);
            return PartialView("_AgentsListPartial", agents);
        }

        public PartialViewResult GetPatients(string name)
        {
            var patients = _patientViewRepository.GetPatientsForAgent(CurrentUser.OwnerId, FilterInformation.AllItemsNoSort,name);
            return PartialView("_PatientListPartial", patients.Result);
        }
    }
}