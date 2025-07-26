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
            base.OnModelCreating(modelBuilder);
            // Code à implémenter si nécessaire
        }
    }
}
