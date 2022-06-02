using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFEmvc.Models
{
    public class Data
    {
        [Key]
        public int DataId { get; set; }

        public string Month { get; set; }

        public string LEONI_Part { get; set; }

        public string Part_Request { get; set; }

        public string Context { get; set; }

        public string Supplier { get; set; }
  
        public string Fors_Material_Group { get; set; }
 
        public string LEONI_Part_Classification { get; set; }

        public check Check { get; set; }



    }
}
