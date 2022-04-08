using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationPFE.Models;

namespace PFEmvc.Models
{
    public class Criterias
    {
        [Key]
        public int CrtId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Environment environment { get; set; }
        public check Check { get; set; }
    }
}
