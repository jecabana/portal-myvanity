using System;
using MyVanity.Domain;

namespace MyVanity.Model.UserModels
{
    public class UserViewModelConverter : IUserModelConverter
    {
        public UserViewModel ConvertToModel(User entity)
        {
            var patient = entity as Patient;
            
            if (patient != null)
            {
                return new UserViewModel
                       {
                           Id = patient.Id,
                           Email = patient.Email,
                           FirstName = patient.Profile.FirstName,
                           LastName = patient.Profile.LastName,
                           MiddleName = patient.Profile.LastName
                       };
            }

            var agent = entity as Agent;

            if (agent != null)
            {
                return new UserViewModel
                {
                    Id = agent.Id,
                    Email = agent.Email,
                    FirstName = agent.PersonDetails.FirstName,
                    LastName = agent.PersonDetails.LastName,
                    MiddleName = agent.PersonDetails.MiddleName
                };
            }

            return null;
        }

        public User ConvertToSource(UserViewModel model)
        {
            throw new NotSupportedException();
        }

        public string GetUserName(User user)
        {
            var asAgent = user as Agent;
            var asPatient = user as Patient;

            if (asAgent != null)
                return asAgent.PersonDetails.FullName;

            return asPatient != null ? asPatient.Profile.FullName : user.UserName;
        }
    }
}
