using System;
using System.Text;
namespace Brief_Bibliotheque.Handlers
{
    public static class PasswordHashHandler
    {
        // Crée un "hash" en combinant password + sel simple et en le convertissant en base64
        public static string HashPassword(string password)
        {
            var salt = "SelStatique123"; // ne jamais faire ça en production
            var saltedPassword = password + salt;
            var bytes = Encoding.UTF8.GetBytes(saltedPassword);
            return Convert.ToBase64String(bytes);
        }

        // Vérifie si le mot de passe correspond au hash enregistré
        public static bool VerifyPassword(string password, string hash)
        {
            var hashedInput = HashPassword(password); // rehash du mot de passe entré
            return hashedInput == hash;
        }
    }
}