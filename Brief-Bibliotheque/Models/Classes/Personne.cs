using System.ComponentModel.DataAnnotations;

namespace Brief_Bibliotheque.Models.Classes
{
    public abstract class Personne
    {
        private string _nom = string.Empty;
        public int Id { get; set; }
        public required string Nom { get => _nom; set => _nom = value.ToUpperInvariant(); } // Invariant est plus sûr (voir doc pour détails)
        public required string Prenom { get; set; }
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public required DateTime DateDeNaissance { get; set; }

    }
}
