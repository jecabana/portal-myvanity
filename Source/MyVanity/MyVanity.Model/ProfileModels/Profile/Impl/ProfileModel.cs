using System;
using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.ProfileModels.Profile.Impl
{
    public class ProfileModel : ModelBase
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        public string FullName 
        {
            get
            {
                var firstName = FirstName.Trim();
                var middleName = MiddleName != null ? MiddleName.Trim() : string.Empty;
                var lastName = LastName.Trim();

                if (!string.IsNullOrEmpty(middleName))
                    return string.Format("{0} {1} {2}", firstName, middleName, lastName);

                return string.Format("{0} {1}", firstName, lastName);
            }
        }
        
        [Required]
        [Display(Name = "Sex")]
        public bool IsMale { get; set; }
        
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }
    }
}
