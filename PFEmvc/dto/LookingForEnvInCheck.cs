using PFEmvc.Models;
using System.Collections.Generic;

namespace PFEmvc.dto
{
    public class LookingForEnvInCheck
    {
        public int checkId { get; set; }
        public string comments { get; set; }
        public string status { get; set; }
        public int envId { get; set; }
        public List<int> DataIds { get; set; }
        public List<int> CriteriaIds { get; set; }
        public Environment Environment { get; set; }
    }
}
