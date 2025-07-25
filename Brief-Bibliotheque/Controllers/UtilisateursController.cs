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
    public class UtilisateursController : Controller
    {
        private readonly BiblioDB _context;

        public UtilisateursController(BiblioDB context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilisateurs.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurs = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateurs == null)
            {
                return NotFound();
            }

            return View(utilisateurs);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateurs utilisateurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilisateurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateurs);
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurs = await _context.Utilisateurs.FindAsync(id);
            if (utilisateurs == null)
            {
                return NotFound();
            }
            return View(utilisateurs);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateurs utilisateurs)
        {
            if (id != utilisateurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilisateurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateursExists(utilisateurs.Id))
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
            return View(utilisateurs);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurs = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateurs == null)
            {
                return NotFound();
            }

            return View(utilisateurs);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilisateurs = await _context.Utilisateurs.FindAsync(id);
            if (utilisateurs != null)
            {
                _context.Utilisateurs.Remove(utilisateurs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateursExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}
