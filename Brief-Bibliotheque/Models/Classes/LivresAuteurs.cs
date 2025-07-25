namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresAuteurs
    {
        int Id {  get; set; }
        public int IdLivres { get; set; }
        public int IdAuteur { get; set; }
        public required Livres Livre { get; set; }
        public required Auteurs Auteur { get; set; }
    }
}