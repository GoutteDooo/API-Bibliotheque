using Brief_Bibliotheque.Models.Classes;

namespace Brief_Bibliotheque.Models
{
    public class Genres
    {
        public int IdGenres { get; set; }
        public required string NomGenres { get; set; }
        public List<Livres> Livres { get; set; } = new List<Livres>();
    }
}
