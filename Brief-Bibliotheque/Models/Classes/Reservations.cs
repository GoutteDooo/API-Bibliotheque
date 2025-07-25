using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Reservations
    {
        public required int Id { get; set;}
        public required DateTime DateReservation { get; set; }
        public int IdUtilisateurs { get; set; }
        public int IdLivres { get; set; }
        public Utilisateurs Utilisateurs { get; set; }
        public Livres Livres { get; set; }
    }
}