using PFEmvc.Models;

namespace PFEmvc.dto
{
    public class FillMasterDetailsChecks
    {
        public int CheckDetailId { get; set; }
        public string CDQM_comments { get; set; }
        public string DQMS_feedback { get; set; }
        public string CDQM_feedback { get; set; }
        public string TopicOwner_feedback { get; set; }
        public string Status { get; set; }
        public Criterias Criteria { get; set; }
        public int? CheckId { get; set; }
    }
}
