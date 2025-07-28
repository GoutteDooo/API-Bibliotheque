using Brief_Bibliotheque.Models.Classes.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class UtilisateurViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Rôle")]
        public required Role Role { get; set; }
        public string Nom { get; set; } = string.Empty;
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = string.Empty;

        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime DateDeNaissance { get; set; }
        public string Mail { get; set; } = string.Empty;
        [Display(Name = "Téléphone")]
        public string? Tel { get; set; }
        [Display(Name = "Numéro de Rue")]
        public string? NumeroDeRue { get; set; }
        [Display(Name = "Nom de Rue")]
        public string? NomDeRue { get; set; }
        public string? Ville { get; set; }
        [Display(Name = "Code Postal")]
        public string? CodePostal { get; set; }
    }
}
