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
    public class ReservationsApiController : ControllerBase
    {
        private readonly BiblioDB _context;

        public ReservationsApiController(BiblioDB context)
        {
            _context = context;
        }

        // Récupère toutes les réservations
        [HttpGet]
        [SwaggerOperation(Summary = "Récupère la liste de tous les réservations")]
        [SwaggerResponse(200, "Succès", typeof(IEnumerable<Auteur>))]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // Récupère toutes les réservations avec l'id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un réservation par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Réservation non trouvé")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if(reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // Créer une nouvelle réservation
        [HttpPost]
        [SwaggerOperation(Summary = "Créer un nouvelle réservation")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        // Modifier une réservation
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier une réservation par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Réservation non trouvé")]
        public async Task<ActionResult<Reservation>> PutReservation (int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest();
            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!_context.Reservations.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // Supprimer une réservation
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer une réservation par son ID")]
        [SwaggerResponse(200, "Succès", typeof(Auteur))]
        [SwaggerResponse(404, "Réservation non trouvé")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
                return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
