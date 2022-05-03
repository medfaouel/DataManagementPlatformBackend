using PFEmvc.Models;

namespace PFEmvc.dto
{
    public class CheckingDetailsDTO
    {
        public int CheckDetailId { get; set; }
        public int criteriaId { get; set; }
        public string CDQM_comments { get; set; }
        public string DQMS_feedback { get; set; }
        public string CDQM_feedback { get; set; }
        public string TopicOwner_feedback { get; set; }
        public string status { get; set; }
        public Criterias criterias { get; set; }
    }
}
