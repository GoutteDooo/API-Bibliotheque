using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Reservation
    {
        public int Id { get; set; }
        [Display(Name = "Date de la réservation")]
        [DataType(DataType.Date)]
        public required DateTime DateReservation { get; set; }
        [Display(Name = "ID du Membre")]
        public int IdUtilisateur { get; set; }
        [Display(Name = "ID du Livre")]
        public int IdLivre { get; set; }
        [Display(Name = "Membre")]
        public required Utilisateur Utilisateur { get; set; }
        public required Livre Livre { get; set; }
    }
}