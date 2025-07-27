using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;

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
        public async Task<IActionResult> Index()
        {
            // Récupérer les Livres et Utilisateurs pour chaque emprunt afin de les envoyer à la vue
            var emprunts = await _context.Emprunts
                .Include(l => l.Livre)
                .Include(l => l.Utilisateur)
                .OrderBy(l => l.RetourEmprunt)
                .ToListAsync();

            return View(emprunts);
        }

        // GET: Emprunts/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Emprunts/Create
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
        public async Task<IActionResult> Create([Bind("Id,DateEmprunt,IdLivre,IdUtilisateur")] EmpruntViewModel empruntVM)
        {
            if (ModelState.IsValid)
            {
                var livre = await _context.Livres.FirstOrDefaultAsync(l => l.Id == empruntVM.IdLivre);
                var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == empruntVM.IdUtilisateur);

                if (livre == null) return Problem("Livre non trouvé :(");
                if (utilisateur == null) return Problem("Membre non trouvé :(");

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
        public async Task<IActionResult> Rendre(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);

            if (emprunt != null)
            {
                emprunt.EstRendu = true;
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

        private bool EmpruntsExists(int id)
        {
            return _context.Emprunts.Any(e => e.Id == id);
        }
    }
}
