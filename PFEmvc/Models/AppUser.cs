using Microsoft.AspNetCore.Identity;
using System;
using WebApplicationPFE.Models;

namespace PFEmvc.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Team Team { get; set; }


        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
