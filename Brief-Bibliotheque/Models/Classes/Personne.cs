using Brief_Bibliotheque.Models.Classes.Enums;
using System.Text.Json.Serialization;

namespace Brief_Bibliotheque.Models.Classes
{
    public abstract class Personne
    {
        public int IdPersonne { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }

    }
}
