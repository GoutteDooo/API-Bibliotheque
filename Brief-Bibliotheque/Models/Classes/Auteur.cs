namespace Brief_Bibliotheque.Models.Classes
{
    public class Auteur : Personne
    {
        public int AuteurId { get; set; }
        public List<Livre> Livres { get; set; } = new List<Livre>();
    }
}
