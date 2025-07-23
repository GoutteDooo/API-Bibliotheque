namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresGenres
    {
        public int IdLivre { get; set; }
        public int IdGenre { get; set; }
        public required Livre Livre { get; set; }
        public required Genre Genre{ get; set; }
    }
}
