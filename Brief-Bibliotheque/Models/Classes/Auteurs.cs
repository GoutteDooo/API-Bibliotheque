namespace Brief_Bibliotheque.Models.Classes
{
    public class Auteurs : Personnes
    {
        public int Id { get; set; }
        public required List<Livres> Livres { get; set; } = new List<Livres>();
    }
}
