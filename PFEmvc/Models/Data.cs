using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFEmvc.Models
{
    public class Data
    {
        [Key]
        public int DataId { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public string LEONI_Part { get; set; }
        [Required]
        public string Part_Request { get; set; }
        [Required]
        public string Contexxt { get; set; }
        [Required]
        public string Supplier { get; set; }
        [Required]
        public string Fors_Material_Group { get; set; }
        [Required]
        public string LEONI_Part_Classification { get; set; }
       


    }
}
