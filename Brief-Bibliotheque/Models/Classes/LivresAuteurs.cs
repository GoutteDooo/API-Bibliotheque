namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresAuteurs
    {
        public int IdLivre { get; set; }
        public int IdAuteur { get; set; }
        public required Livre Livre { get; set; }
        public required Auteur { get; set; }
    }
}
