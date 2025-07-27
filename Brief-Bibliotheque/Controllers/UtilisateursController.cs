using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Classes.Enums;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // On passe chaque utilisateur dans sa ViewModel car on ne veut pas envoyer son mot de passe dans la vue
            var model = await _context.Utilisateurs
                .Select(u => new UtilisateurViewModel
                {
                    Id = u.Id,
                    Role = u.Role,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    DateDeNaissance = u.DateDeNaissance,
                    Mail = u.Mail,
                    Tel = u.Tel,
                    NumeroDeRue = u.NumeroDeRue,
                    NomDeRue = u.NomDeRue,
                    Ville = u.Ville,
                    CodePostal = u.CodePostal,
                }).ToListAsync();

                return View(model);
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
            // Cast les rôles dans le ViewBag pour pouvoir les afficher dans la vue
            ViewBag.Roles = Enum.GetValues(typeof(Role)) // Obtient l'array suivant : Array { Role.Membre, Role.Employe, Role.Administrateur }
                .Cast<Role>() // Convertir chaque élément de l'array en type Role (sinon object par défaut) => IEnumarable<Role>
                .Select(r => new SelectListItem // Pour chaque Role r, on crée un nouvel object SelectListItem contenant : 
                {
                    Value = ((int)r).ToString(), // la valeur envoyée au serveur quand l'utilisateur choisit cette option (conversion en entier puis en chaîne)
                    Text = r.ToString() // le texte affiché à l'écran dans la balise <select> (on récupère le nom textuel de l'enum ; "Membre" par exemple)
                }).ToList();

            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateur utilisateurs)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateur utilisateurs)
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
