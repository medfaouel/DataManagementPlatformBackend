using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PFEmvc.Models;
using WebApplicationPFE.Models;

namespace PFEmvc
{
    public class DbContextApp :IdentityDbContext<AppUser,IdentityRole, string>
    {
        public DbContextApp(DbContextOptions<DbContextApp> options):base(options)
        {

        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<check> Checks { get; set; }
        public DbSet<Data> Data { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<Criterias> Criterias { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        

    }
}
