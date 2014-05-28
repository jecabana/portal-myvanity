using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyVanity.Domain;
using MyVanity.Model.AppointmentModels;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories.AppointmentViewsRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentViewRepository _viewRepository;
        private readonly IAppointmentModelConverter _modelConverter;

        public AppointmentsController(IAppointmentViewRepository viewRepository, IMembershipService membershipService, IAppointmentModelConverter modelConverter) : base(membershipService)
        {
            _viewRepository = viewRepository;
            _modelConverter = modelConverter;
        }

        public ActionResult Index()
        {
            var appointments = _viewRepository.GetAppointmentsForAgent(CurrentUser.OwnerId);
            var model = new AppointmentsIndexModel(appointments);

            return View(model);
        }

        public ActionResult Create()
        {
            var model = _modelConverter.BuildModel(new AppointmentEditModel());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AppointmentEditModel model)
        {
            if (ModelState.IsValid)
            {
                _viewRepository.Insert(model);
                return RedirectToAction("Index");
            }
            model = _modelConverter.BuildModel(model);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _viewRepository.FindById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AppointmentEditModel model)
        {
            if (ModelState.IsValid)
            {
                _viewRepository.Update(model);
                return RedirectToAction("Index");
            }
            model = _modelConverter.BuildModel(model);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _viewRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult GetStatuses()
        {
            var result = new Dictionary<int, string>();

            foreach (var item in Enum.GetValues(typeof(AppointmentStatus)))
            {
                var status = (AppointmentStatus) item;
                if (status == AppointmentStatus.Scheduled || status == AppointmentStatus.Hidden)
                    continue;

                result.Add((int)item, Enum.GetName(typeof(AppointmentStatus), item));
            }

            return Json(new { statuses = result.Select(x => new {val = x.Key, name = x.Value}) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveStatus(AppointmentStatus newStatus, int id)
        {
            var status = _viewRepository.ChangeStatus(id, newStatus);
            return Json(new { newStatus = status }, JsonRequestBehavior.AllowGet);
        }
    }
}