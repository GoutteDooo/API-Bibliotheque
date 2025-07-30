using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Classes.Enums;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brief_Bibliotheque.Handlers;

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

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null) return Problem("Utilisateur non trouvé!");

            // Récupérer les emprunts en cours correspondant à l'id de l'utilisateur
            var emprunts = await _context.Emprunts
                .Include(e => e.Livre)
                .Where(e => e.IdUtilisateur == utilisateur.Id && !e.EstRendu)
                .ToListAsync();

            var utilisateurVM = new UtilisateurViewModel
            {
                Role = utilisateur.Role,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                DateDeNaissance = utilisateur.DateDeNaissance,
                Mail = utilisateur.Mail,
                Tel = utilisateur.Tel,
                NumeroDeRue = utilisateur.NumeroDeRue,
                NomDeRue = utilisateur.NomDeRue,
                Ville = utilisateur.Ville,
                CodePostal = utilisateur.CodePostal,
                Emprunts = emprunts // si c'est null c'est pas grave, on gère l'affichage
            };

            if (utilisateurVM == null)
            {
                return NotFound();
            }

            return View(utilisateurVM);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            // Cast les rôles dans le ViewBag pour pouvoir les afficher dans la vue
            ViewBag.Roles = ObtenirViewBagRoles();

            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                // Vérifier que le même email n'existe pas dans la bdd
                var emailValide = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Mail == utilisateur.Mail);
                if (emailValide is not null) return Problem("Problème lors de la création du compte");

                // Hacher le mdp donné par l'utilisateur
                utilisateur.MotDePasse = PasswordHashHandler.HashPassword(utilisateur.MotDePasse);

                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurs = await _context.Utilisateurs.FindAsync(id);

            ViewBag.Roles = ObtenirViewBagRoles();

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateursExists(utilisateur.Id))
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
            return View(utilisateur);
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

        /**
         * Retourne les rôles de la classe enum Role pour l'affichage en liste déroulante avec <select></select>
         */
        private static List<SelectListItem> ObtenirViewBagRoles()
        {
            return Enum.GetValues(typeof(Role)) // Obtient l'array suivant : Array { Role.Membre, Role.Employe, Role.Administrateur }
                .Cast<Role>() // Convertir chaque élément de l'array en type Role (sinon object par défaut) => IEnumarable<Role>
                .Select(r => new SelectListItem // Pour chaque Role r, on crée un nouvel object SelectListItem contenant : 
                {
                    Value = ((int)r).ToString(), // la valeur envoyée au serveur quand l'utilisateur choisit cette option (conversion en entier puis en chaîne)
                    Text = r.ToString() // le texte affiché à l'écran dans la balise <select> (on récupère le nom textuel de l'enum ; "Membre" par exemple)
                }).ToList();
        }
    }
}
