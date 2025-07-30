using Brief_Bibliotheque.Models.Classes.Enums;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Utilisateur : Personne
    {
        public int UtilisateurId { get; set; }
        [Display(Name = "Téléphone")]
        [StringLength(14, MinimumLength = 8)]
        [RegularExpression(@"^\d{6,14}$", ErrorMessage = "Un n° de téléphone doit contenir entre 6 et 14 chiffres.")]
        public string? Tel { get; set; }
        [Display(Name = "Numéro de Rue")]
        public string? NumeroDeRue { get; set; }
        [Display(Name = "Nom de Rue")]
        [StringLength(100, MinimumLength = 6)]
        public string? NomDeRue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Role Role { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Veuillez saisir un mot de passe contenant plus de 10 caractères svp.")]
        public required string MotDePasse { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,7})+)$", ErrorMessage = "L'adresse Mail n'est pas une adresse valide.")]
        [StringLength(70, MinimumLength = 9)]
        public required string Mail { get; set; }
        public string? Ville { get; set; }
        [Display(Name = "Code Postal")]
        [StringLength(5)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Le code postal doit contenir exactement 5 chiffres.")]

        public string? CodePostal { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
