using System.ComponentModel.DataAnnotations;
using Brief_Bibliotheque.Models.Classes;

namespace Brief_Bibliotheque.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Display(Name = "Nom du genre")]
        [StringLength(50)]
        [Required]
        public string NomGenre { get; set; } = "Autre";
        public List<Livre> Livres { get; set; } = new List<Livre>();
    }
}
