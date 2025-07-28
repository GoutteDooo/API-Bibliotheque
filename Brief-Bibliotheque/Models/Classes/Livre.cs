using System.ComponentModel.DataAnnotations;
namespace Brief_Bibliotheque.Models.Classes
{
    public class Livre
    {
        public int Id { get; set; }
        [StringLength(13)]
        public required string Isbn { get; set; }
        [StringLength(50)]
        public required string Titre { get; set; }
        [Display(Name = "Année de publication")]
        [Range(-1000,2026)]
        public required int AnneePublication { get; set; }
        public required string Etat { get; set; }
        [Display(Name = "En cours d'emprunt")]
        public bool EstEmprunte { get; set; }
        [Display(Name = "Reservé")]
        public bool EstReserve { get; set; }
        [Display(Name = "Disponible")]
        public bool EstDisponible { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
        public required List<Genre> Genres { get; set; } = new List<Genre>();
        public required List<Auteur> Auteurs { get; set; } = new List<Auteur>();

        /*      public List<LivresGenres> LivresGenres { get; set; } = new List<LivresGenres>();
                public List<LivresAuteurs> LivreAuteurs{ get; set; } = new List<LivresAuteurs>();*/
    }
}
