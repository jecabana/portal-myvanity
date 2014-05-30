using MyVanity.Domain;
using MyVanity.Model.ProfileModels.Profile.Impl;

namespace MyVanity.Model.ProfileModels.Profile
{
    public interface IProfileModelConverter : IModelConverter<PersonDetails, ProfileModel>
    {
    }
}
