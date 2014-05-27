using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.UserModels;
using MyVanity.Services.Membership;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Admin, Agent")]
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityConverter<User, UserViewModel> _modelConverter; 

        public UserController(IMembershipService membershipService, IUnitOfWork unitOfWork, IEntityConverter<User, UserViewModel> modelConverter)
            : base(membershipService)
        {
            _unitOfWork = unitOfWork;
            _modelConverter = modelConverter;
        }

        public PartialViewResult GetToUsers(string criteria, bool exceptCurrentUser = true)
        {
            var agents = _unitOfWork.GetRepository<Agent>().Get(x => x.UserName.Contains(criteria)
                                                                  || x.PersonDetails.FirstName.Contains(criteria)
                                                                  || x.PersonDetails.MiddleName.Contains(criteria)
                                                                  || x.PersonDetails.LastName.Contains(criteria));

            int number;
            Int32.TryParse(criteria, out number);
            var patients = _unitOfWork.GetRepository<Patient>().Get(x => x.UserName.Contains(criteria)
                                                                  || x.Profile.FirstName.Contains(criteria)
                                                                  || x.Profile.MiddleName.Contains(criteria)
                                                                  || x.Profile.LastName.Contains(criteria)
                                                                  || x.Id == number).ToList();

            var agentModel = agents.Select(x => _modelConverter.ConvertToModel(x));
            var patientModel = patients.Select(x => _modelConverter.ConvertToModel(x));

            var model = new List<UserViewModel>(agentModel);
            model.AddRange(patientModel);

            if (exceptCurrentUser)
                model = model.Where(x => x.Id != CurrentUser.OwnerId).ToList();

            return PartialView("_UserListPartial", model);
        }

    }
}