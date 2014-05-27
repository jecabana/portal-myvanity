using System.ComponentModel.DataAnnotations;
using MyVanity.Domain;
using MyVanity.Model.ProfileModels.Profile.Impl;

namespace MyVanity.Model.DoctorModels.Impl
{
    public class DoctorEditModel : ModelBase
    {
        [Display(Name = "Small Bio")]
        [Required]
        public string Description { get; set; }

        public string Email { get; set; }

        public ProfileModel Profile { get; set; }

        [Display(Name = "Picture")]
        public string PicPath { get; set; }

        public string FullName 
        {
            get { return Profile.FullName; }
        }
    }
}
