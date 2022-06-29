using System;
using WebApplicationPFE.Models;

namespace PFEmvc.dto
{
    public class AppUserDTO
    {
        public AppUserDTO(string firstName, string lastName, string email, string userName,DateTime dateCreated,string role,string id,Team team=null
            )
        {
            FirstName = firstName;
            LastName =lastName;
            Email =email;
            UserName =userName;
            DateCreated = dateCreated;
            Role = role;
            Id = id;
            Team = team;

        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public Team Team { get; set; }
    }
}
