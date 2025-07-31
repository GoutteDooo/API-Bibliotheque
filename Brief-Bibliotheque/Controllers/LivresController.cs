using Brief_Bibliotheque.Models;
using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brief_Bibliotheque.Controllers
{
    public class LivresController : Controller
    {
        private readonly BiblioDB _context;

        public LivresController(BiblioDB context)
        {
            _context = context;
        }

        // GET : Livres/Index?recherche={recherche}
        /**
         * Affiche les détails du livre recherché s'il existe, sinon retourne une erreur 404
         */
        public async Task<IActionResult> Index(string? recherche, string? genre)
        {
            if (_context.Livres == null) return Problem("Aucun livre dans la base de données");

            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.WriteLine("RECHERCHE : " + recherche);

            // Récupérer les livres avec leurs auteurs et genres
            var query = _context.Livres
                .Include(l => l.Auteurs)
                .Include(l => l.Genres)
                .AsQueryable();


            if (!string.IsNullOrEmpty(recherche))
            {

                query = query.Where(l => l.Titre.ToUpper().Contains(recherche.ToUpper()));

            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(l => l.Genres.Any(g => g.NomGenre.ToUpper().Contains(genre.ToUpper())));

            }
            var livres = await query.ToListAsync();

            if (!livres.Any() && (!string.IsNullOrEmpty(recherche) || !string.IsNullOrEmpty(genre)))
            {
                return Problem("Le livre recherché n'existe pas :(");
            }

            return View(livres);
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livres = await _context.Livres
                .Include(l => l.Auteurs)
                .Include(l => l.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livres == null)
            {
                return NotFound();
            }

            return View(livres);
        }

        // GET: Livres/Create
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = await _context.Genres.ToListAsync();
            ViewBag.Auteurs = await _context.Auteurs.ToListAsync();
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Create(
            [Bind("Id,Isbn,Titre,AnneePublication,Etat,EstEmprunte,EstReserve,EstDisponible")] Livre livres,
            int[] selectedGenres,
            int[] selectedAuteurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livres);
                await _context.SaveChangesAsync();

                // Associer les genres sélectionnés
                if (selectedGenres != null && selectedGenres.Length > 0)
                {
                    var genres = await _context.Genres
                        .Where(g => selectedGenres.Contains(g.Id))
                        .ToListAsync();
                    livres.Genres = genres;
                }

                // Associer les auteurs sélectionnés
                if (selectedAuteurs != null && selectedAuteurs.Length > 0)
                {
                    var auteurs = await _context.Auteurs
                        .Where(a => selectedAuteurs.Contains(a.Id))
                        .ToListAsync();
                    livres.Auteurs = auteurs;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recharger les listes en cas d'erreur
            ViewBag.Genres = await _context.Genres.ToListAsync();
            ViewBag.Auteurs = await _context.Auteurs.ToListAsync();
            return View(livres);
        }
        // GET: Livres/Edit/5
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livres = await _context.Livres.FindAsync(id);
            if (livres == null)
            {
                return NotFound();
            }
            return View(livres);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Isbn,Titre,AnneePublication,Etat,EstEmprunte,EstReserve,EstDisponible")] Livre livres)
        {
            if (id != livres.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livres);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivresExists(livres.Id))
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
            return View(livres);
        }

        // GET: Livres/Delete/5
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livres = await _context.Livres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livres == null)
            {
                return NotFound();
            }

            return View(livres);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employé")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livres = await _context.Livres.FindAsync(id);
            if (livres != null)
            {
                _context.Livres.Remove(livres);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivresExists(int id)
        {
            return _context.Livres.Any(e => e.Id == id);
        }
    }
}
