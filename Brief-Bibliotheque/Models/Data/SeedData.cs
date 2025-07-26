using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Brief_Bibliotheque.Models.Classes;
namespace Brief_Bibliotheque.Models.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BiblioDB(
            serviceProvider.GetRequiredService<
                DbContextOptions<BiblioDB>>()))
        {
            Console.WriteLine("Initializer ACTIF");
            /* LIVRES */
            if (!context.Livres.Any()) // S'il n'y a pas de livres présents, on remplit la seed
            {
                var rowling = new Auteur
                {
                    Nom = "J.K.",
                    Prenom = "Rowling",
                    DateDeNaissance = DateTime.Parse("1965-7-31")
                };
                var verne = new Auteur
                {
                    Nom = "Verne",
                    Prenom = "Jules",
                    DateDeNaissance = DateTime.Parse("1828-2-8")
                };
                var fantaisie = new Genre { NomGenre = "Fantaisie" };
                var aventure = new Genre { NomGenre = "Aventure" };
                context.Livres.AddRange(
                    new Livre
                    {
                        Isbn = "1",
                        Titre = "Harry Potter à l'école des sorciers",
                        AnneePublication = 1997,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
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
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
                        }
                    },
                    new Livre
                    {
                        Isbn = "3",
                        Titre = "Harry Potter et le Prisonnier d'Azkaban",
                        AnneePublication = 1999,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
                        }
                    },
                    new Livre
                    {
                        Isbn = "4",
                        Titre = "Harry Potter et la Coupe de feu",
                        AnneePublication = 2000,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
                        }
                    },
                    new Livre
                    {
                        Isbn = "5",
                        Titre = "Harry Potter et l'Ordre du phénix",
                        AnneePublication = 2003,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
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
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
                        }
                    },
                    new Livre
                    {
                        Isbn = "7",
                        Titre = "Harry Potter et les Reliques de la Mort",
                        AnneePublication = 2007,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            fantaisie
                        },
                        Auteurs = new List<Auteur> {
                            rowling
                        }
                    },
                    new Livre
                    {
                        Isbn = "8",
                        Titre = "Le Tour du monde en quatre-vingt jours",
                        AnneePublication = 1872,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            aventure
                        },
                        Auteurs = new List<Auteur> {
                            verne
                        }
                    },
                    new Livre
                    {
                        Isbn = "9",
                        Titre = "Vingt Mille Lieues sous les mers",
                        AnneePublication = 1870,
                        Etat = "Neuf",
                        EstEmprunte = false,
                        EstReserve = false,
                        EstDisponible = true,
                        Genres = new List<Genre>
                        {
                            fantaisie,
                            aventure
                        },
                        Auteurs = new List<Auteur> {
                            verne
                        }
                    }
                );
            }
            context.SaveChanges();
        }
    }
}