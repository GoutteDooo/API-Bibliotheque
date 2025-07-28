using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Brief_Bibliotheque.Controllers.API
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmpruntsApiController : ControllerBase
    {
        private readonly BiblioDB _context;

        public EmpruntsApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupère tous les emprunts en cours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunts()
        {
            return await _context.Emprunts.ToListAsync();
        }

        // Récupère les emprunts en cours avec l'id
        [HttpGet("{id}")]
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
        public async Task<ActionResult<Emprunt>> PostEmprunt(Emprunt emprunt)
        {
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmprunt), new { id = emprunt.Id }, emprunt);
        }

        // Modifier un emprunt
        [HttpPut("{id}")]
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
