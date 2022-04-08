using Microsoft.AspNetCore.Identity;
using System;

namespace PFEmvc.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
