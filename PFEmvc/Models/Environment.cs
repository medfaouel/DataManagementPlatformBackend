using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationPFE.Models;

namespace PFEmvc.Models
{
    public class Environment
    {
        [Key]
        public int EnvId { get; set; }
        [Required]
        public string EnvName { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Team> Teams { get; set; }
        public List<check> Checks { get; set; }

    }
}
