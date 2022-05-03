using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("CriteriaId")]
        public Criterias Criteria { get; set; }
        [ForeignKey("CheckId")]
        public check? Check { get; set; }
        public int? CheckId { get; set; }
    }
}
