namespace Brief_Bibliotheque.Models.Classes
{
    public class Emprunts
    {
        public int IdEmprunts { get; set; }
        public required DateTime DateEmprunt { get; set; }
        public bool EstRendu { get; set; }
        public required DateTime RetourEmprunt { get; set; }
        public int IdLivres { get; set; }
        public required Livres Livres { get; set; }

        public int IdUtilisateurs { get; set; }
        public required Utilisateurs Utilisateurs { get; set; }

    }
}
