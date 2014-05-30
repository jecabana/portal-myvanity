using System.ComponentModel.DataAnnotations;
using MyVanity.Domain;
using MyVanity.Model.ProfileModels.Profile.Impl;

namespace MyVanity.Model.AgentModels.Impl
{
    public class AgentEditModel : ModelBase
    {
        public AgentType Type { get; set; }

        public ProfileModel ProfileDetails { get; set; }

        public string FullName
        {
            get { return ProfileDetails.FullName; }
        }

        [Display(Name = "Picture")]
        public string PicPath { get; set; }
        
        [Display(Name = "Small Bio")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "User Name")]
        public string UserName
        {
            get { return Email; }
        }

        [Required]
        public string Email { get; set; }
    }
}
