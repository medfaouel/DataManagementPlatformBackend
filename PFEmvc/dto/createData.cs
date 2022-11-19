using System.Collections.Generic;

namespace PFEmvc.dto
{
    public class createData
    {
        public int teamid { get; set; }
        public int DataId { get; set; }
        public int checkId { get; set; }
        public List<int> criteriaIds { get; set; }
        public string Month { get; set; }
        public string LEONI_Part { get; set; }
        public string Part_Request { get; set; }
        public string Context { get; set; }
        public string Supplier { get; set; }
        public string Fors_Material_Group { get; set; }
        public string LEONI_Part_Classification { get; set; }
    }
}
