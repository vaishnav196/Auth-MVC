using AuthMVC.Models;
using Microsoft.EntityFrameworkCore;



namespace AuthMVC.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> users{ get; set; }
    }
}
