namespace Brief_Bibliotheque.Models.Classes
{
    public class LivresAuteurs
    {
        public int LivreId { get; set; }
        public int AuteurId { get; set; }
        public Livre Livre { get; set; }
        public Auteur Auteur { get; set; }
    }
}
