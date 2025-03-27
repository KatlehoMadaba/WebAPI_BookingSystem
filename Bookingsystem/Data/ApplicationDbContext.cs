using Bookingsystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookingsystem.Data
{
    public class ApplicationDbContext:DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base (options) //Only if you have multple db contexts you specfiy the type of db context
 
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
       public DbSet<User> Users { get; set; }
       
    }
}
