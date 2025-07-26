using System.ComponentModel.DataAnnotations;

namespace Brief_Bibliotheque.Models.Classes
{
    public abstract class Personne
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public required DateTime DateDeNaissance { get; set; }

    }
}
