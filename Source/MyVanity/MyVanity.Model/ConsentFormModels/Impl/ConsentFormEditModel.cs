using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.ConsentFormModels.Impl
{
    public class ConsentFormEditModel : ModelBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Body")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must upload the document first")]
        public string FileGuid { get; set; }
    }
}
