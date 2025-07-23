using Brief_Bibliotheque.Models.Classes;

namespace Brief_Bibliotheque.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public List<LivresGenres> LivresGenres { get; set; } = new List<LivresGenres>();
    }
}
