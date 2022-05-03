using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFEmvc.Models
{
    public class check
    {

        [Key]
        public int CheckId { get; set; }
        public string CheckAddress { get; set; }
        public string Status { get; set; }
        public string CDQM_comments { get; set; }
        public string DQMS_feedback { get; set; }
        public string CDQM_feedback { get; set; }
        public string TopicOwner_feedback { get; set; }

        public int DataIdentity { get; set; }
        [ForeignKey("DataId")]
        public Data Data { get; set; }
        public Environment environment { get; set; }
        public List<Criterias> Criterias { get; set; }
    }
}
