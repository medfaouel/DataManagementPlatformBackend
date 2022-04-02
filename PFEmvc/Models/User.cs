using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPFE.Models
{
    public class User :IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string LoginStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


    }
}
