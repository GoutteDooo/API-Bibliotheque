using Brief_Bibliotheque.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Brief_Bibliotheque.Services
{
    public class UtilisateurService
    {
        private readonly BiblioDB _context;

        public UtilisateurService(BiblioDB context)
        {
            _context = context;
        }

        public async Task SupprimerUtilisateurAsync(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null) return;

            // Remettre les livres en cours d'emprunt comme disponibles
            var empruntsEnCours = await _context.Emprunts
                .Include(e => e.Livre)
                .Where(e => e.IdUtilisateur == id && !e.EstRendu)
                .ToListAsync();

            foreach (var emprunt in empruntsEnCours)
            {
                if (emprunt.Livre != null)
                {
                    emprunt.Livre.EstEmprunte = false;
                    emprunt.Livre.EstDisponible = true;
                }
            }

            // Supprimer tous les emprunts liés (contrainte FK)
            var tousLesEmprunts = _context.Emprunts.Where(e => e.IdUtilisateur == id);
            _context.Emprunts.RemoveRange(tousLesEmprunts);

            // Remettre les livres en cours de réservation comme disponibles
            var reservationsEnCours = await _context.Reservations
                .Include(r => r.Livre)
                .Where(r => r.IdUtilisateur == id && !r.EstTermine)
                .ToListAsync();

            foreach (var reservation in reservationsEnCours)
            {
                if (reservation.Livre != null)
                {
                    reservation.Livre.EstReserve = false;
                    reservation.Livre.EstDisponible = true;
                }
            }

            // Supprimer toutes les réservations liées (contrainte FK)
            var toutesLesReservations = _context.Reservations.Where(r => r.IdUtilisateur == id);
            _context.Reservations.RemoveRange(toutesLesReservations);

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
        }
    }
}