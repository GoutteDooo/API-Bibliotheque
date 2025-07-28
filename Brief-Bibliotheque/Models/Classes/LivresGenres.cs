namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresGenres
    {
        int Id { get; set; }
        public int IdLivre { get; set; }
        public int IdGenre { get; set; }
        public required Livre Livre { get; set; }
        public required Genre Genre{ get; set; }
    }
}