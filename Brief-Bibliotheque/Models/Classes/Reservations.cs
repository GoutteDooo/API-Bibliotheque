using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Reservations
    {
        public required IdReservations { get; set;}
        public required DateTime DateReservation { get; set; }
        public int IdUtilisateurs { get; set; }
        public int IdLivres { get; set; }
        public required Utilisateurs Utilisateurs { get; set; }
        public required Livres Livres { get; set; }
    }
}
