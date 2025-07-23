namespace Brief_Bibliotheque.Models.Classes
{
    public class Auteurs : Personne
    {
        public required List<LivresAuteurs> LivresAuteurs { get; set; } = new List<LivresAuteurs>();
    }
}
