using Brief_Bibliotheque.Models.Classes.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class UtilisateurViewModel
    {
        public int Id { get; set; }
        public required Role Role { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public DateTime DateDeNaissance { get; set; }
        public string Mail { get; set; } = string.Empty;
        public string? Tel { get; set; }
        public string? NumeroDeRue { get; set; }
        public string? NomDeRue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }
    }
}
