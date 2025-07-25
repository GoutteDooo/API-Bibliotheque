namespace Brief_Bibliotheque.Models.Classes
{
    public class Livres
    {
        public int Id { get; set; }
        public required string Isbn { get; set; }
        public required string Titre { get; set; }
        public required int AnneePublication { get; set; }
        public required string Etat { get; set; }
        public bool EstEmprunter { get; set; }
        public bool EstReserve { get; set; }
        public bool EstDisponible { get; set; }

        public List<Reservations> Reservations { get; set; } = new List<Reservations>();
        public List<Emprunts> Emprunts { get; set; } = new List<Emprunts>();
        public List<Genres> Genres { get; set; } = new List<Genres>();
        public List<Auteurs> Auteurs { get; set; } = new List<Auteurs>();

        /*      public List<LivresGenres> LivresGenres { get; set; } = new List<LivresGenres>();
                public List<LivresAuteurs> LivreAuteurs{ get; set; } = new List<LivresAuteurs>();*/
    }
}
