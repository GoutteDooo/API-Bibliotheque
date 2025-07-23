namespace Brief_Bibliotheque.Models.Classes
{
    public class Emprunts
    {
        public int IdEmprunts { get; set; }
        public DateTime Date_Emprunt { get; set; }
        public bool Est_Rendu { get; set; }
        public DateTime Retour_Emprunt { get; set; }
        public int LivreId { get; set; }
        public required Livres Livres { get; set; }

        public int IdUtilisateur { get; set; }
        public required Utilisateur Utilisateur { get; set; }

    }
}
