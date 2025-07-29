using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Brief_Bibliotheque.Services;

namespace Brief_Bibliotheque.Controllers
{
    public class AuthController : Controller
    {
        private readonly BiblioDB _context;
        private readonly JwtService _jwtService;

        public AuthController(BiblioDB context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // GARDER : Gestion des utilisateurs (Index, Details, Edit, Delete)
        // GET: Auth
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilisateurs.ToListAsync());
        }

        // GET: Auth/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Nom,Prenom,DateDeNaissance")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                // Vérifier si l'email existe déjà
                if (await _context.Utilisateurs.AnyAsync(u => u.Mail == utilisateur.Mail))
                {
                    ViewBag.Error = "Cet email est déjà utilisé";
                    return View(utilisateur);
                }

                _context.Add(utilisateur);
                await _context.SaveChangesAsync();

                // Après inscription, connecter automatiquement
                var token = _jwtService.GenerateToken(utilisateur.Nom, utilisateur.Role.ToString());

                Response.Cookies.Append("jwt-token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("Index", "Home");
            }
            return View(utilisateur);
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string nom, string motDePasse)
        {
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(motDePasse))
            {
                ViewBag.Error = "Veuillez remplir tous les champs";
                return View();
            }

            var user = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Nom == nom && u.MotDePasse == motDePasse);

            if (user == null)
            {
                ViewBag.Error = "Nom ou mot de passe incorrect";
                return View();
            }

            var token = _jwtService.GenerateToken(user.Nom, user.Role.ToString());

            Response.Cookies.Append("jwt-token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt-token");
            return RedirectToAction("Index", "Home");
        }

        // GARDER : Méthodes existantes pour la gestion des utilisateurs
        // GET: Auth/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return View(utilisateur);
        }

        // POST: Auth/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilisateurId,Tel,NumeroDeRue,NomDeRue,Role,MotDePasse,Mail,Ville,CodePostal,Id,Nom,Prenom,DateDeNaissance")] Utilisateur utilisateur)
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
                    if (!UtilisateurExists(utilisateur.Id))
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

        // GET: Auth/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: Auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}