using System.ComponentModel.DataAnnotations;

namespace Brief_Bibliotheque.Models.Classes
{
    public class EmpruntViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Date de l'emprunt")]
        public required DateTime DateEmprunt { get; set; } = DateTime.Today;
        [Display(Name = "ID du Livre")]
        public int IdLivre { get; set; }
        [Display(Name = "ID du Membre")]

        public int IdUtilisateur { get; set; }

        public EmpruntViewModel() {}
    }
}
