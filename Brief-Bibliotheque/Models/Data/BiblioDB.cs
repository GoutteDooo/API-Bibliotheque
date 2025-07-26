using Brief_Bibliotheque.Models.Classes;
using Microsoft.EntityFrameworkCore;
namespace Brief_Bibliotheque.Models.Data
{
    public class BiblioDB : DbContext
    {
        public BiblioDB(DbContextOptions<BiblioDB> options) : base(options)
        {
        }
        public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();
        public DbSet<Livre> Livres => Set<Livre>();
        public DbSet<Emprunt> Emprunts => Set<Emprunt>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Auteur> Auteurs => Set<Auteur>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration many-to-many Livre-Genre
            modelBuilder.Entity<Livre>()
                .HasMany(l => l.Genres)
                .WithMany(g => g.Livres);

            // Configuration many-to-many Livre-Auteur  
            modelBuilder.Entity<Livre>()
                .HasMany(l => l.Auteurs)
                .WithMany(a => a.Livres);

            base.OnModelCreating(modelBuilder);
        }
    }
}
