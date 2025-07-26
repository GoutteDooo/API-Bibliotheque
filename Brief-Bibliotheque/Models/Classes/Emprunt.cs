namespace Brief_Bibliotheque.Models.Classes
{
    public class Emprunt
    {
        public int Id { get; set; }
        public required DateTime DateEmprunt { get; set; }
        public bool EstRendu { get; set; }
        public required DateTime RetourEmprunt { get; set; }
        public int IdLivres { get; set; }
        public required Livre Livres { get; set; }

        public int IdUtilisateur { get; set; }
        public required Utilisateur Utilisateurs { get; set; }

    }
}
