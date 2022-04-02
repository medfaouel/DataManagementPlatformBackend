using PFEmvc.Models;

namespace PFEmvc.dto
{
    public class LookingForEnvInCheck
    {
        public int checkId { get; set; }
        public string comments { get; set; }
        public string status { get; set; }
        public int envId { get; set; }
        public Environment Environment { get; set; }
    }
}
