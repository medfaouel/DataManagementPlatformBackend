using PFEmvc.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPFE.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string TeamDescription { get; set; }
        public Environment environment { get; set; }
        public List<Worker> workers { get; set; }
        public List<Administrator> administrators { get; set; }
        public List<Criterias> criterias { get; set; }
    }
}
