using Brief_Bibliotheque.Models.Classes.Enums;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Utilisateurs : Personne
    {
        public int IdUtilisateur { get; set; }
        public string Tel { get; set; }
        public string NumeroDeRue { get; set; }
        public string NomDeRue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
        public string MotDePasse { get; set; }
        public string Mail { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }

    }
}
