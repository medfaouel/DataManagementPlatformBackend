using PFEmvc.Models;

namespace PFEmvc.dto
{
    public class LookingForEnvInCriterias
    {
        public int crtId { get; set; }
        public int envId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Environment env { get; set; }
    }
}
