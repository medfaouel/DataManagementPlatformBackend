using System.ComponentModel.DataAnnotations;

namespace PFEmvc.dto
{
    public class ConfirmEmailViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
