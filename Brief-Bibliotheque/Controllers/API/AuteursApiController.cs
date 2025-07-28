using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auteurs>>> GetAuteurs()
        {
            return await _context.Auteurs.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Auteurs>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }
        [HttpPost]
        public async Task<ActionResult<Auteurs>> PostAuteur(Auteurs auteur)
        {
            _context.Auteurs.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuteur), new { id = auteur.Id }, auteur);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuteur(int id, Auteurs auteur)
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
        [HttpDelete("{id}")]
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
