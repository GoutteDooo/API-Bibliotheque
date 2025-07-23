using Brief_Bibliotheque.Models.Classes.Enums;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Utilisateurs : Personnes
    {
        public int IdUtilisateurs { get; set; }
        public required string Tel { get; set; }
        public required string NumeroDeRue { get; set; }
        public required string NomDeRue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Role Role { get; set; }
        public required string MotDePasse { get; set; }
        public required string Mail { get; set; }
        public required string Ville { get; set; }
        public required string CodePostal { get; set; }

        public List<Reservations> Reservations { get; set; } = new List<Reservations>();
    }
}
