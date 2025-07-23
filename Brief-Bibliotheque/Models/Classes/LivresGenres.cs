namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresGenres
    {
        public int LivreId { get; set; }
        public int GenreId { get; set; }
        public Livre Livre { get; set; }
        public Genre Genre{ get; set; }
    }
}
