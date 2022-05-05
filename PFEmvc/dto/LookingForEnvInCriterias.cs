using PFEmvc.Models;
using WebApplicationPFE.Models;

namespace PFEmvc.dto
{
    public class LookingForEnvInCriterias
    {
        public int crtId { get; set; }
        public int dataId { get; set; }

        public int TeamId { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Data data { get; set; }
        public Team team { get; set; }
    }
}

