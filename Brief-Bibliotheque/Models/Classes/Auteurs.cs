namespace Brief_Bibliotheque.Models.Classes
{
    public class Auteurs : Personnes
    {
        public required List<LivresAuteurs> LivresAuteurs { get; set; } = new List<LivresAuteurs>();
    }
}
