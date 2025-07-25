namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresGenres
    {
        int Id { get; set; }
        public int IdLivres { get; set; }
        public int IdGenres { get; set; }
        public required Livres Livre { get; set; }
        public required Genres Genre{ get; set; }
    }
}