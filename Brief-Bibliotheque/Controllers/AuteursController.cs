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
    public class AuteursController : Controller
    {
        private readonly BiblioDB _context;

        public AuteursController(BiblioDB context)
        {
            _context = context;
        }

        // GET: Auteurs
        public async Task<IActionResult> Index()
        {
            // Récupérer chaque auteur de manière distincte
            var auteurs = await _context.Auteurs.Distinct().ToListAsync();
            foreach (var auteur in auteurs)
            {
                Console.WriteLine(auteur.Id);
            }
            return View(auteurs);
        }

        // GET: Auteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auteurs = await _context.Auteurs
                .Include(l => l.Livres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auteurs == null)
            {
                return NotFound();
            }

            return View(auteurs);
        }

        // GET: Auteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auteurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,DateDeNaissance")] Auteur auteurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auteurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auteurs);
        }

        // GET: Auteurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auteurs = await _context.Auteurs.FindAsync(id);
            if (auteurs == null)
            {
                return NotFound();
            }
            return View(auteurs);
        }

        // POST: Auteurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,DateDeNaissance")] Auteur auteurs)
        {
            if (id != auteurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auteurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuteursExists(auteurs.Id))
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
            return View(auteurs);
        }

        // GET: Auteurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auteurs = await _context.Auteurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auteurs == null)
            {
                return NotFound();
            }

            return View(auteurs);
        }

        // POST: Auteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auteurs = await _context.Auteurs.FindAsync(id);
            if (auteurs != null)
            {
                _context.Auteurs.Remove(auteurs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuteursExists(int id)
        {
            return _context.Auteurs.Any(e => e.Id == id);
        }
    }
}
