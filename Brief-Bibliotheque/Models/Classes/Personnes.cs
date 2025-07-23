using Brief_Bibliotheque.Models.Classes.Enums;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public abstract class Personnes
    {
        public int IdPersonnes { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required DateTime DateDeNaissance { get; set; }

    }
}
