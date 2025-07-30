using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Brief_Bibliotheque.Models.Classes;
using Brief_Bibliotheque.Handlers;
using Npgsql.Internal;
namespace Brief_Bibliotheque.Models.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BiblioDB(serviceProvider.GetRequiredService<DbContextOptions<BiblioDB>>()))
        {
            Console.WriteLine(
                "!! ATTENTION ! TOUTES LES DONNEES VONT ETRE REINITIALISEES !!\n" +
                "!! AJOUTEZ UN VALIDATEUR OU METTRE SUR OFF CETTE METHODE DANS Program.cs SI VOUS NE SOUHAITEZ PLUS QUE CELA SE FASSE !!");
            // Vider entièrement toutes les tables avant de les réinitialiser
            context.Auteurs.ExecuteDelete();
            context.Genres.ExecuteDelete();
            context.Emprunts.ExecuteDelete();
            context.Reservations.ExecuteDelete();
            context.Livres.ExecuteDelete();
            context.Utilisateurs.ExecuteDelete();

            /* AUTEURS */
            var aRowling = new Auteur
            {
                Nom = "J.K.",
                Prenom = "Rowling",
                DateDeNaissance = DateTime.Parse("1965-7-31")
            };
            var aVerne = new Auteur
            {
                Nom = "Verne",
                Prenom = "Jules",
                DateDeNaissance = DateTime.Parse("1828-2-8")
            };
            var aHugo = new Auteur
            {
                Nom = "Hugo",
                Prenom = "Victor",
                DateDeNaissance = DateTime.Parse("1802-2-26")
            };
            context.Auteurs.AddRange(aRowling, aVerne, aHugo);
            /* GENRES */
            var gFantaisie = new Genre { NomGenre = "Fantaisie" };
            var gAventure = new Genre { NomGenre = "Aventure" };
            var gHistorique = new Genre { NomGenre = "Historique" };
            context.Genres.AddRange(gFantaisie, gAventure);
            /* UTILISATEURS */
            var uToto = new Utilisateur
            {
                Nom = "Toto",
                Prenom = "Tutu",
                DateDeNaissance = DateTime.Parse("2000-12-25"),
                Tel = "0123456789",
                NumeroDeRue = "22",
                NomDeRue = "Rue de la Blague",
                Role = Classes.Enums.Role.Employé,
                MotDePasse = PasswordHashHandler.HashPassword("ZéroPlusZéro"),
                Mail = "latete@toto.haha",
                Ville = "TotoLand",
                CodePostal = "12345"
            };
            var uAlice = new Utilisateur
            {
                Nom = "Airline",
                Prenom = "Alice",
                DateDeNaissance = DateTime.Parse("2002-11-22"),
                Tel = "0123456789",
                NumeroDeRue = "22",
                NomDeRue = "Rue de la Beauté",
                Role = Classes.Enums.Role.Membre,
                MotDePasse = PasswordHashHandler.HashPassword("AliceInWonderland"),
                Mail = "alice@test.com",
                Ville = "Paris",
                CodePostal = "75001"
            };
            var uBob = new Utilisateur
            {
                Nom = "Bricoleur",
                Prenom = "Bob",
                DateDeNaissance = DateTime.Parse("1989-3-31"),
                Tel = "0987654321",
                NumeroDeRue = "17",
                NomDeRue = "Rue du Travail",
                Role = Classes.Enums.Role.Membre,
                MotDePasse = PasswordHashHandler.HashPassword("bobbricoleur"),
                Mail = "bob@brico.depot",
                Ville = "Brico Dépôt",
                CodePostal = "10000"
            };
            
            context.Utilisateurs.AddRange(uToto, uAlice, uBob,
                new Utilisateur
                {
                    Nom = "dupont",
                    Prenom = "jean",
                    DateDeNaissance = DateTime.Parse("1992-7-21"),
                    NumeroDeRue = "11",
                    NomDeRue = "Rue de la paix",
                    Role = Classes.Enums.Role.Administrateur,
                    MotDePasse = PasswordHashHandler.HashPassword("test1234"),
                    Mail = "jean@dupont.eau",
                    Ville = "Paris",
                    CodePostal = "75001"
                });
            /* LIVRES */
            context.Livres.AddRange(
                new Livre
                {
                    Isbn = "1",
                    Titre = "Harry Potter à l'école des sorciers",
                    AnneePublication = 1997,
                    Etat = "Neuf",
                    EstEmprunte = true,
                    EstReserve = true,
                    EstDisponible = false,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "2",
                    Titre = "Harry Potter et la Chambre des secrets",
                    AnneePublication = 1998,
                    Etat = "Neuf",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "3",
                    Titre = "Harry Potter et le Prisonnier d'Azkaban",
                    AnneePublication = 1999,
                    Etat = "Bon",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "4",
                    Titre = "Harry Potter et la Coupe de feu",
                    AnneePublication = 2000,
                    Etat = "Bon",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "5",
                    Titre = "Harry Potter et l'Ordre du phénix",
                    AnneePublication = 2003,
                    Etat = "Usé",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "6",
                    Titre = "Harry Potter et le Prince de sang-mêlé",
                    AnneePublication = 2005,
                    Etat = "Neuf",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "7",
                    Titre = "Harry Potter et les Reliques de la Mort",
                    AnneePublication = 2007,
                    Etat = "Dégradé",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie
                    },
                    Auteurs = new List<Auteur> {
                            aRowling
                    }
                },
                new Livre
                {
                    Isbn = "8",
                    Titre = "Le Tour du monde en quatre-vingt jours",
                    AnneePublication = 1872,
                    Etat = "Usé",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gAventure
                    },
                    Auteurs = new List<Auteur> {
                            aVerne
                    }
                },
                new Livre
                {
                    Isbn = "9",
                    Titre = "Vingt Mille Lieues sous les mers",
                    AnneePublication = 1870,
                    Etat = "Usé",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                            gFantaisie,
                            gAventure
                    },
                    Auteurs = new List<Auteur> {
                            aVerne
                    }
                },
                new Livre
                {
                    Isbn = "10",
                    Titre = "Les Misérables",
                    AnneePublication = 1862,
                    Etat = "Bon",
                    EstEmprunte = false,
                    EstReserve = false,
                    EstDisponible = true,
                    Genres = new List<Genre>
                    {
                        gHistorique
                    },
                    Auteurs = new List<Auteur> {
                            aVerne
                    }
                }
            );
            context.SaveChanges(); //On save pour pouvoir utiliser les méthodes First() qui viennent

            /* EMPRUNTS */
            var livreEmprunt = context.Livres.First(); // Premier livre de la table Livres
            var utilisateurEmprunt = context.Utilisateurs.First(); // Premier Utilisatuer de la table Utilisateurs
            var emprunt = new Emprunt
            {
                DateEmprunt = DateTime.Parse("2025-7-27"),
                EstRendu = false,
                RetourEmprunt = DateTime.Parse("2025-8-10"),
                Livre = livreEmprunt,                 
                IdLivre = livreEmprunt.Id,
                Utilisateur = utilisateurEmprunt,
                IdUtilisateur = utilisateurEmprunt.Id,
            };
            context.Emprunts.Add( emprunt );
            /* RESERVATIONS */
            var reservation = new Reservation
            {
                DateReservation = DateTime.Parse("2025-7-27"),
                DateFinReservation = DateTime.Parse("2025-8-17"),
                EstTermine = false,
                Utilisateur = uBob,
                IdUtilisateur = uBob.Id,
                Livre = livreEmprunt,
                IdLivre = livreEmprunt.Id,
            };
            context.Reservations.Add(reservation);

            // Enregistrer les changements dans la bdd
            context.SaveChanges();
        }
    }
}