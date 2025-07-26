using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Reservation
    {
        public required int Id { get; set;}
        public required DateTime DateReservation { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdLivre { get; set; }
        public required Utilisateur Utilisateur { get; set; }
        public required Livre Livre { get; set; }
    }
}