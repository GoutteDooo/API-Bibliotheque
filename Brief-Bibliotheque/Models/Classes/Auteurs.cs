namespace Brief_Bibliotheque.Models.Classes
{
    public class Auteurs : Personnes
    {
        public required List<Livres> Livres { get; set; } = new List<Livres>();

    }
}
