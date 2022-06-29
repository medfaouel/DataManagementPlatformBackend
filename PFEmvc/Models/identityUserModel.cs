using WebApplicationPFE.Models;

namespace PFEmvc.dto
{
    public class identityUserModel
    {
        public string userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Team Team { get; set; }
    }
}
