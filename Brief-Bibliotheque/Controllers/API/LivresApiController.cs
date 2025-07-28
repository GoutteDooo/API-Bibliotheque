using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            return await _context.Livres.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivres(int id)
        {
            var livre = await _context.Livres.FindAsync(id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }
        [HttpPost]
        public async Task<ActionResult<Livre>> PostAuteur(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivres), new { id = livre.Id }, livre);
        }
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
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
