using System.ComponentModel.DataAnnotations;

namespace PFEmvc.Models
{
    public class CheckDetails
    {
        [Key]
        public int CheckDetailId { get; set; }
        public string CDQM_comments { get; set; }
        public string DQMS_feedback { get; set; }
        public string CDQM_feedback { get; set; }
        public string TopicOwner_feedback { get; set; }
        public string status { get; set; }
    }
}
