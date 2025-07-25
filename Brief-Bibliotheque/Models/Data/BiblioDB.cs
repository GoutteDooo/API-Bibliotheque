using Brief_Bibliotheque.Models.Classes;
using Microsoft.EntityFrameworkCore;
namespace Brief_Bibliotheque.Models.Data
{
    public class BiblioDB : DbContext
    {
        public BiblioDB(DbContextOptions<BiblioDB> options) : base(options)
        {
        }
        public DbSet<Utilisateurs> Utilisateurs => Set<Utilisateurs>();
        public DbSet<Livres> Livres => Set<Livres>();
        public DbSet<Emprunts> Emprunts => Set<Emprunts>();
        public DbSet<Reservations> Reservations => Set<Reservations>();
        public DbSet<Auteurs> Auteurs => Set<Auteurs>();
        public DbSet<Genres> Genres => Set<Genres>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration many-to-many Livre-Genre
            modelBuilder.Entity<Livres>()
                .HasMany(l => l.Genres)
                .WithMany(g => g.Livres);

            // Configuration many-to-many Livre-Auteur  
            modelBuilder.Entity<Livres>()
                .HasMany(l => l.Auteurs)
                .WithMany(a => a.Livres);

            base.OnModelCreating(modelBuilder);
        }
    }
}
