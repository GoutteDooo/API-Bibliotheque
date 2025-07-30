using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace Brief_Bibliotheque.Controllers.API
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "Administrateur")]
    public class EmpruntsApiController : ControllerBase
    {
        private readonly BiblioDB _context;

        public EmpruntsApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupère tous les emprunts en cours
        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les emprunts")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Auteur>))]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunts()
        {
            return await _context.Emprunts.ToListAsync();
        }

        // Récupère les emprunts en cours avec l'id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un enmprunt par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Emprunt non trouvé")]
        public async Task<ActionResult<Emprunt>> GetEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return emprunt;
        }

        // Créer un emprunt
        [HttpPost]
        [SwaggerOperation(Summary = "Créer un nouvel emprunt")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        public async Task<ActionResult<Emprunt>> PostEmprunt(Emprunt emprunt)
        {
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmprunt), new { id = emprunt.Id }, emprunt);
        }

        // Modifier un emprunt
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier un emprunt par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Emprunt non trouvé")]
        public async Task<ActionResult<Emprunt>> PutEmprunt(int id, Emprunt emprunt)
        {
            if (id != emprunt.Id)
                return BadRequest();
            _context.Entry(emprunt).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Emprunts.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // Supprimer un emprunt
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer un emprunt par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "emprunt non trouvé")]
        public async Task<ActionResult<Emprunt>> DeleteEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
                return NotFound();
            _context.Emprunts.Remove(emprunt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
