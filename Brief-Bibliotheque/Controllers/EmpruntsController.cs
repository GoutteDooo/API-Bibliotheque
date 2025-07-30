using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Brief_Bibliotheque.Controllers
{
    public class EmpruntsController : Controller
    {
        private readonly BiblioDB _context;

        public EmpruntsController(BiblioDB context)
        {
            _context = context;
        }

        // GET: Emprunts
        public async Task<IActionResult> Index(bool? enCours = null)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = _context.Emprunts
                    .Include(e => e.Livre)
                    .Include(e => e.Utilisateur)
                    .AsQueryable();

            if (User.IsInRole("Membre") && int.TryParse(userId, out int parsedId))
            {
                query = query.Where(e => e.IdUtilisateur == parsedId);
            }

            // Filtrer selon le statut si spécifié
            if (enCours.HasValue)
            {
                if (enCours.Value) // emprunts en cours
                {
                    query = query.Where(e => !e.EstRendu);  // EstRendu = false
                }
                else // emprunts terminés
                {
                    query = query.Where(e => e.EstRendu);   // EstRendu = true
                }
            }

            var emprunts = await query
                .OrderBy(e => e.RetourEmprunt)  // Tri par date de retour
                .ToListAsync();

            return View(emprunts);
        }

        // GET: Emprunts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var emprunts = await _context.Emprunts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (emprunts == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(emprunts);
        //}

        // GET: Emprunts/Create
        [Authorize(Roles = "Administrateur, Employé")]
        public IActionResult Create()
        {
            // Retourner un EmpruntViewModel pour n'afficher que les propriétés intéressantes
            var empruntViewModel = new EmpruntViewModel() { DateEmprunt = DateTime.UtcNow };
            return View(empruntViewModel);
        }

        // POST: Emprunts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Create([Bind("Id,DateEmprunt,IdLivre,IdUtilisateur")] EmpruntViewModel empruntVM)
        {
            if (ModelState.IsValid)
            {
                var livre = await _context.Livres.FirstOrDefaultAsync(l => l.Id == empruntVM.IdLivre);
                var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == empruntVM.IdUtilisateur);

                if (livre == null) return Problem("Livre non trouvé :(");
                if (utilisateur == null) return Problem("Membre non trouvé :(");

                // Vérifier si le livre est déjà emprunté
                // Si c'est le cas, annuler l'emprunt
                if (livre.EstEmprunte) return Problem("Livre déjà emprunté!");
                // Sinon, vérifier s'il est déjà réservé
                if (livre.EstReserve)
                {
                    // Si c'est le cas, aller retrouver la réservation associée
                    var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.IdLivre == livre.Id);
                    // Si réservation non trouvée, on annule la réservation
                    if (reservation == null) return Problem("Le livre est réservé, mais la réservation n'a pas été trouvée...");

                    // Si la réservation a été trouvée,
                    // Vérifier si l'id réservateur est différent de l'id de l'emprunteur
                    if (reservation.IdUtilisateur != utilisateur.Id) return Problem("Une réservation existe déjà pour un autre utilisateur !");
                    // Si c'est le cas, annuler l'emprunt
                    // Sinon, terminer la réservation
                    reservation.EstTermine = true;
                }
                // Sinon c'est tout bon, le livre est disponible

                // Modifier les propriétés EstEmprunte et EstDisponible du livre.
                // EstReserve est automatiquement reset sur false
                livre.EstDisponible = false;
                livre.EstEmprunte = true;
                livre.EstReserve = false;

                var emprunt = new Emprunt
                {
                    DateEmprunt = empruntVM.DateEmprunt,
                    EstRendu = false,
                    RetourEmprunt = empruntVM.DateEmprunt.AddDays(14),
                    IdLivre = livre.Id,
                    Livre = livre,
                    IdUtilisateur = utilisateur.Id,
                    Utilisateur = utilisateur
                };



                _context.Add(emprunt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(empruntVM);
        }

        // GET: Emprunts/Edit/5
        /**
         * Cette méthode Edit n'édite que la date d'emprunt (ajoute 7 jours), car c'est la seule donnée pertinente à pouvoir être éditée
         */
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return NotFound();
            }

            // Appliquer le prolongement du prêt
            emprunt.RetourEmprunt = emprunt.RetourEmprunt.AddDays(7);
            // Si l'emprunt a été réservé par un tier, prolonger également la date de réservation
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.IdLivre == emprunt.IdLivre && !r.EstTermine && !emprunt.EstRendu);
            // Si une réservation correspondante a été trouvée, alors prolonger DateFinReservation de 7 jours
            if (reservation != null)
            {
                Console.WriteLine("Reservation dateFin avant : " + reservation.DateFinReservation);
                reservation.DateFinReservation = reservation.DateFinReservation.AddDays(7);
                Console.WriteLine("Reservation dateFin après : " + reservation.DateFinReservation);
                _context.Update(reservation);
            }

            try
            {
                _context.Update(emprunt);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpruntsExists(emprunt.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Emprunts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateEmprunt,EstRendu,RetourEmprunt,IdLivre,IdUtilisateur")] Emprunt emprunt)
        {
            if (id != emprunt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprunt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpruntsExists(emprunt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emprunt);
        }

        // GET: Emprunts/Delete/5
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprunts = await _context.Emprunts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprunts == null)
            {
                return NotFound();
            }

            return View(emprunts);
        }

        // POST: Emprunts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt != null)
            {
                _context.Emprunts.Remove(emprunt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET : Emprunts/Rendu
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Rendre(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null) return Problem("Emprunt non trouvé!");
            // Trouver le livre associé
            var livreEmprunte = await _context.Livres.FirstOrDefaultAsync(l => l.Id == emprunt.IdLivre);
            if (livreEmprunte == null) return Problem("Livre non trouvé !");

            livreEmprunte.EstEmprunte = false;
            livreEmprunte.EstDisponible = true;
            emprunt.EstRendu = true;

            try
            {
                _context.Update(emprunt);
                _context.Update(livreEmprunte);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpruntsExists(emprunt.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmpruntsExists(int id)
        {
            return _context.Emprunts.Any(e => e.Id == id);
        }
    }
}
