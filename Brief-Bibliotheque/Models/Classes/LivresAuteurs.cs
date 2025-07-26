namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresAuteurs
    {
        int Id {  get; set; }
        public int IdLivre { get; set; }
        public int IdAuteur { get; set; }
        public required Livre Livre { get; set; }
        public required Auteur Auteur { get; set; }
    }
}