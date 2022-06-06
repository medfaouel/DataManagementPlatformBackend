using System.ComponentModel.DataAnnotations;

namespace PFEmvc.dto
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
