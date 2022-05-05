using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
       
        public Team Team { get; set; }
        public Data Data { get; set; }
    }
}
