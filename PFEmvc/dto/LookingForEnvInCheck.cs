using PFEmvc.Models;
using System.Collections.Generic;

namespace PFEmvc.dto
{
    public class LookingForEnvInCheck
    {
        public int checkId { get; set; }
        public string CheckAddress { get; set; }
        public string status { get; set; }
        public string CDQM_comments { get; set; }
        public string DQMS_feedback { get; set; }
        public string CDQM_feedback { get; set; }
        public string TopicOwner_feedback { get; set; }
        public int envId { get; set; }
        public List<int> DataIds { get; set; }
    }
}
