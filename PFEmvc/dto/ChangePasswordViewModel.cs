using System.ComponentModel.DataAnnotations;

namespace PFEmvc.dto
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
