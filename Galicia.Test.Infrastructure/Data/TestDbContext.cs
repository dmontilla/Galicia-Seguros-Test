using Galicia.Test.Core.Entitie;
using Microsoft.EntityFrameworkCore;

namespace Galicia.Test.Infrastructure.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }
        public TestDbContext(DbContextOptions options):base(options)
        {
        }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Domicilio> Domicilios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entitiy =>
            {
                entitiy.HasOne<Domicilio>(e => e.Domicilio)
                       .WithOne(e => e.Persona)
                       .OnDelete(DeleteBehavior.Cascade);

                entitiy.HasIndex(e => new { e.DNI, e.Telefono }).IsUnique(true);

            });

            modelBuilder.Entity<Domicilio>(entity =>
            {
                entity.HasOne<Persona>(e => e.Persona)
                    .WithOne(e => e.Domicilio)
                    .HasForeignKey<Domicilio>(e => e.IdPersona);

            });
        }
    }
}
