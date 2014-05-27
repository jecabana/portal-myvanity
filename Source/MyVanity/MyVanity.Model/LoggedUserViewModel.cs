using System.ComponentModel.DataAnnotations;
using MyVanity.Common.Localization;

namespace MyVanity.Model
{
    public class LoggedUserViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User name", ResourceType = typeof(LocalizerResource))]
        public string UserName { get; set; }
    }
}
