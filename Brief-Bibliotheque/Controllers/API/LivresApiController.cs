using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Brief_Bibliotheque.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresApiController : ControllerBase
    {
        private readonly BiblioDB _context;

        public LivresApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupère tous les livres disponibles
        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les livres")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Auteur>))]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            return await _context.Livres.ToListAsync();
        }

        // Récupère le livre avec son ID
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un livre par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Livre non trouvé")]
        public async Task<ActionResult<Livre>> GetLivres(int id)
        {
            var livre = await _context.Livres.FindAsync(id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }

        // Créer un nouveau livre
        [HttpPost]
        [SwaggerOperation(Summary = "Créer un nouveau livre")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        public async Task<ActionResult<Livre>> PostAuteur(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivres), new { id = livre.Id }, livre);
        }

        // Modifier les informations a un livre
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier un livre par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Livre non trouvé")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.Id)
                return BadRequest();
            _context.Entry(livre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Livres.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // Supprimer un livre
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer un livre par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Livre non trouvé")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
                return NotFound();
            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
