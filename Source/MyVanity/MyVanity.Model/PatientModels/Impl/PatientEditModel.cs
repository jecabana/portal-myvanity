using System.ComponentModel.DataAnnotations;
using System.Web;
using MyVanity.Model.ProfileModels.Contact.Impl;
using MyVanity.Model.ProfileModels.Profile.Impl;

namespace MyVanity.Model.PatientModels.Impl
{
    public class PatientEditModel : ModelBase
    {
        public PatientEditModel()
        {
            Contact = new ContactModel();
            Profile = new ProfileModel();
            CurrentAgent = string.IsNullOrEmpty(CurrentAgent) ? HttpContext.Current.User.Identity.Name : CurrentAgent;
        }

        [Required(ErrorMessage = "Please, enter a patient number")]
        public string PatientNumber { get; set; }

        //Represents current posible online agent username
        public string CurrentAgent { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Surgical Coordinator")]
        public string AgentName { get; set; }

        public ContactModel Contact { get; set; }

        public ProfileModel Profile { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName 
        {
            get
            {
                return Email;
            }
        }

        public string PicPath { get; set; }

        [Required]
        public string Email { get; set; }

        public string FullName 
        {
            get { return Profile.FullName; }
        }

        public string FormattedAddress
        {
            get { return string.Format("{0} {1}", Contact.Address1, Contact.Address2); }
        }

        public string FormattedPhone 
        {
            get { return Contact.Phone.Mobile; }
        }
    }
}
