using Microsoft.EntityFrameworkCore;
using Web.Models.Entity;

namespace Web.Models.Db
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options){}
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}