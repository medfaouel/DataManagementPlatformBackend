using System.Collections.Generic;
using WebApplicationPFE.Models;

namespace PFEmvc.dto
{
    public class CreateEnv
    {
        public int EnvId { get; set; }
        public string EnvName { get; set; }

        public string Description { get; set; }
        public List<int> teamIds { get; set; }
        public List<int> CriteriaIds { get; set; }
        public List<int> ChecksIds { get; set; }

        public List<Team> Teams { get; set; }
    }
}
