using PFEmvc.Models;
using System.Collections.Generic;
using WebApplicationPFE.Models;

namespace PFEmvc.dto
{
    public class CreateTeam
    {
        public int teamId { get; set; }
        public string teamName { get; set; }
        public string teamDescription { get; set; }
        public int envId { get; set; }
        public List<int> workerIds { get; set; }
        public List<int> criteriaIds { get; set; }
        public Environment Env { get; set; }
        

    }
}
