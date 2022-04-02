using WebApplicationPFE.Models;

namespace PFEmvc.dto
{
    public class CreateWorker
    {
        public int UserId { get; set; }

        public int teamId { get; set; }

        public string FirstName { get; set; }

        public string UserName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string LoginStatus { get; set; }
        public Team team { get; set; }

    }
}
