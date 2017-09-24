using Microsoft.EntityFrameworkCore;
using Neppo.Models;

namespace Neppo.Contexts
{
    public class Context: DbContext
    {
        public DbSet<Pessoa> pessoa {get; set;}

        public Context(DbContextOptions<Context> context) : base(context)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pessoa>()
                    .HasIndex(u => u.documento)
                    .IsUnique();
        }
    }
}
