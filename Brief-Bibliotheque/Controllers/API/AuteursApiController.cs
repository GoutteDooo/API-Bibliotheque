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
    public class AuteursApiController : ControllerBase
    {
        private readonly BiblioDB _context;

        public AuteursApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupère tous les auteurs disponibles
        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les auteurs")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Auteur>))]
        public async Task<ActionResult<IEnumerable<Auteur>>> GetAuteurs()
        {
            return await _context.Auteurs.ToListAsync();
        }

        // Récupère tous les auteurs avec l'id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un auteur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Auteur non trouvé")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        // Créer un nouvel auteur
        [HttpPost]
        [SwaggerOperation(Summary = "Créer un nouvel auteur")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        public async Task<ActionResult<Auteur>> PostAuteur(Auteur auteur)
        {
            _context.Auteurs.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuteur), new { id = auteur.Id }, auteur);
        }

        // Modifier un auteur
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier un auteur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Auteur non trouvé")]
        public async Task<IActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.Id)
                return BadRequest();
            _context.Entry(auteur).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Auteurs.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // Supprimer un auteur
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer un auteur par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Auteur non trouvé")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
                return NotFound();
            _context.Auteurs.Remove(auteur);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
