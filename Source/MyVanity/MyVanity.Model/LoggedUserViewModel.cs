using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model
{
    public class LoggedUserViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
