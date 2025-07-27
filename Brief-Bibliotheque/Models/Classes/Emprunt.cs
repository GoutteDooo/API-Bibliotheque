using System.ComponentModel.DataAnnotations;

namespace Brief_Bibliotheque.Models.Classes
{
    public class Emprunt
    {
        public int Id { get; set; }
        [Display(Name = "Date de l'emprunt")]
        [DataType(DataType.Date)]
        public required DateTime DateEmprunt { get; set; }
        [Display(Name = "Emprunt rendu")]
        public bool EstRendu { get; set; }
        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public required DateTime RetourEmprunt { get; set; }
        [Display(Name = "ID du livre")]
        public int IdLivres { get; set; }
        [Required]
        public required Livre Livre { get; set; }

        [Display(Name = "ID du Membre")]
        public int IdUtilisateur { get; set; }
        [Display(Name = "Membre")]
        public required Utilisateur Utilisateur { get; set; }

    }
}
