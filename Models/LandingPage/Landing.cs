using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GlucoSeeTracker.Models.LandingPage
{
    public class Landing : DbContext
    {
       public Landing(DbContextOptions<Landing> options) 
            : base(options) { } 

        public DbSet<User> users { get; set; }
        public DbSet<Password> passwords { get; set; }


    }

}
