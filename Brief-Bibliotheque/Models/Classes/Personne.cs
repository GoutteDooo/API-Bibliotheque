namespace Brief_Bibliotheque.Models.Classes
{
    public abstract class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
    }
}
