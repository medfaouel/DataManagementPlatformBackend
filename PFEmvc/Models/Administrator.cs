using System.ComponentModel.DataAnnotations;

namespace WebApplicationPFE.Models
{
    public class Administrator : User
    {
        [Required]
        public string Job { get; set; }

    }
}
