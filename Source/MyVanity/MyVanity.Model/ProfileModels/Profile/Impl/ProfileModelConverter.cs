using MyVanity.Domain;

namespace MyVanity.Model.ProfileModels.Profile.Impl
{
    public class ProfileModelConverter : IProfileModelConverter
    {
        public ProfileModel ConvertToModel(PersonDetails entity)
        {
            return new ProfileModel
                   {
                       DOB = entity.DOB,
                       FirstName = entity.FirstName,
                       LastName = entity.LastName,
                       MiddleName = entity.MiddleName,
                       IsMale = entity.Sex == Sex.Male,
                   };
        }

        public PersonDetails ConvertToSource(ProfileModel model)
        {
            return new PersonDetails
                   {
                       DOB = model.DOB,
                       FirstName = model.FirstName,
                       LastName = model.LastName,
                       MiddleName = model.MiddleName,
                       Sex = model.IsMale ? Sex.Male : Sex.Female
                   };
        }
    }
}
