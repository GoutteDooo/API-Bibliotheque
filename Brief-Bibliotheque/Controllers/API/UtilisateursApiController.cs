using Brief_Bibliotheque.Models;
using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Brief_Bibliotheque.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursApiController : ControllerBase
    {
        private readonly BiblioDB _context;

        public UtilisateursApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupérer tous les utilisateurs
        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les utilisateurs")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Auteur>))]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        // Récupére tous les utilisateurs avec l'id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un utilisateur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Utilisateur non trouvé")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // Créer un utilisateur
        [HttpPost]
        [SwaggerOperation(Summary = "Créer un nouveau utilisateur")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id }, utilisateur);
        }

        // Modifier les données d'un utilisateur
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier un utilisateur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Utilisateur non trouvé")]
        public async Task<ActionResult<Utilisateur>> PutUtilisateur (int id , Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
                return BadRequest();
            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Utilisateurs.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // Supprimer un utilisateur
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer un utilisateur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Utilisateur non trouvé")]
        public async Task<ActionResult<Utilisateur>> DeleteUtilisateur (int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
                return NotFound();

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
