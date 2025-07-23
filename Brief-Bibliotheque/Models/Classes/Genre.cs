using Brief_Bibliotheque.Models.Classes;

namespace Brief_Bibliotheque.Models
{
    public class Genre
    {
        public int IdGenres { get; set; }
        public required string NomGenres { get; set; }
        public List<LivresGenres> LivresGenres { get; set; } = new List<LivresGenres>();
    }
}
