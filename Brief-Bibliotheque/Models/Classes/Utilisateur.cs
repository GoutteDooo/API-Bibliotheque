using Brief_Bibliotheque.Models.Classes.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Utilisateur : Personne
    {
        public int UtilisateurId { get; set; }
        [Display(Name = "Téléphone")]
        [StringLength(14, MinimumLength = 8)]
        public string? Tel { get; set; }
        [Display(Name = "Numéro de Rue")]
        public string? NumeroDeRue { get; set; }
        [Display(Name = "Nom de Rue")]
        [StringLength(100, MinimumLength = 6)]
        public string? NomDeRue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Role Role { get; set; }
        [Required]
        [StringLength(50)]
        public required string MotDePasse { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [StringLength(70, MinimumLength = 9)]
        public required string Mail { get; set; }
        public string? Ville { get; set; }
        [Display(Name = "Code Postal")]
        [StringLength(5)]
        public string? CodePostal { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
