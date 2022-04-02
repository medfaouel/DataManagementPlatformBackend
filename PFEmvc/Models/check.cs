using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PFEmvc.Models
{
    public class check
    {
        [Key]
        public int CheckId { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string Status { get; set; }

        public List<Data> Data { get; set; }
        public Environment environment { get; set; }
    }
}
