using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.ProfileModels.Contact.Impl
{
    public class ContactModel : ModelBase
    {
        public ContactModel()
        {
            Phone = new PhoneModel();
            Social = new SocialModel();
        }

        public PhoneModel Phone { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        public SocialModel Social { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
    }

    public class SocialModel
    {
        public string Facebook { get; set; }

        public string Twitter { get; set; }
    }

    public class PhoneModel
    {
        public string Home { get; set; }

        public string Work { get; set; }

        public string Mobile { get; set; }
    }
}
