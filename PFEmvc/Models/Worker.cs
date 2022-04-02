    using System.ComponentModel.DataAnnotations;
using WebApplicationPFE.Models;

namespace PFEmvc.Models
{
    public class Worker : User
    {
        public Team Team { get; set; }

    }
}
