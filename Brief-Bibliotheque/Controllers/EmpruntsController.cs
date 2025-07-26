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
            return View(await _context.Emprunts.ToListAsync());
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
            return View();
        }

        // POST: Emprunts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateEmprunt,EstRendu,RetourEmprunt,IdLivres,IdUtilisateurs")] Emprunt emprunts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprunts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emprunts);
        }

        // GET: Emprunts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprunts = await _context.Emprunts.FindAsync(id);
            if (emprunts == null)
            {
                return NotFound();
            }
            return View(emprunts);
        }

        // POST: Emprunts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateEmprunt,EstRendu,RetourEmprunt,IdLivres,IdUtilisateurs")] Emprunt emprunts)
        {
            if (id != emprunts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprunts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpruntsExists(emprunts.Id))
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
            return View(emprunts);
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
            var emprunts = await _context.Emprunts.FindAsync(id);
            if (emprunts != null)
            {
                _context.Emprunts.Remove(emprunts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpruntsExists(int id)
        {
            return _context.Emprunts.Any(e => e.Id == id);
        }
    }
}
