using System;

namespace PFEmvc.dto
{
    public class AppUserDTO
    {
        public AppUserDTO(string firstName, string lastName, string email, string userName,DateTime dateCreated,string role )
        {
            FirstName = firstName;
            LastName =lastName;
            Email =email;
            UserName =userName;
            DateCreated = dateCreated;
            Role = role;

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
