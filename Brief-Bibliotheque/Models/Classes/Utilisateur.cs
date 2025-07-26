using Brief_Bibliotheque.Models.Classes.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Utilisateur : Personne
    {
        public int UtilisateurId { get; set; }
        [Display(Name = "Téléphone")]
        public required string Tel { get; set; }
        [Display(Name = "Numéro de Rue")]
        public required string NumeroDeRue { get; set; }
        [Display(Name = "Nom de Rue")]
        public required string NomDeRue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Role Role { get; set; }
        public required string MotDePasse { get; set; }
        public required string Mail { get; set; }
        public required string Ville { get; set; }
        [Display(Name = "Code Postal")]
        public required string CodePostal { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
