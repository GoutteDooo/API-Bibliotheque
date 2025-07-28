using Brief_Bibliotheque.Models;
using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Brief_Bibliotheque.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresApiController : ControllerBase
    {
        private readonly BiblioDB _context;


        public GenresApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupère tous les genres disponibles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // Récupère tous les genres avec l'id
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            
            if(genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // Créer un nouveau genre
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        // Modifier un genre
        [HttpPut("{id}")]
        public async Task<ActionResult<Genre>> PutGenre(int id, Genre genre)
        {
            if (id != genre.Id)
                return BadRequest();
            _context.Entry(genre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Genres.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // Supprimer un genre
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return NotFound();
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
