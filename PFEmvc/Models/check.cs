using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationPFE.Models;

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

        public List<Data> Data { get; set; }
        public List<CheckDetails> CheckDetails { get; set; }
        public Team Team { get; set; }
    }
}
