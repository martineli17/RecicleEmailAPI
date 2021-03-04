using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ContextEmail : DbContext
    {
        public ContextEmail(DbContextOptions<ContextEmail> options) : base(options)
        {
        }
        public DbSet<Email> Email { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }
    }
}