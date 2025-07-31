# Système de Gestion de Bibliothèque

## Description

Ce projet est un système de gestion de bibliothèque développé avec ASP.NET Core, permettant la gestion des livres, des membres et des emprunts/réservations. L'application propose une interface web intuitive avec différents niveaux d'accès selon le type d'utilisateur.

## Technologies utilisées

- **Backend** : ASP.NET Core, C#
- **ORM** : Entity Framework Core
- **Base de données** : SQLite
- **Frontend** : Razor Pages, Bootstrap
- **Authentification** : JWT (JSON Web Tokens)
- **Documentation API** : Swagger
- **Framework** : .NET

## Fonctionnalités par type d'utilisateur

### Utilisateur non connecté
- Consultation du catalogue de la bibliothèque
- Recherche de livres

### Membre
- Toutes les fonctionnalités de l'utilisateur non connecté
- Consultation de ses réservations en cours
- Consultation de ses emprunts en cours et historique

### Employé
- Toutes les fonctionnalités du membre
- Gestion des membres (ajout, modification, suppression)
- Consultation de tous les emprunts et réservations
- Ajout de nouveaux livres au catalogue
- Gestion des emprunts et retours

### Administrateur
- Accès complet à toutes les fonctionnalités du système
- Gestion des employés et des droits d'accès
- Configuration avancée du système

## Installation

### Prérequis
- Visual Studio

### Étapes d'installation

1. **Cloner le repository**
   ```bash
   git clone [URL_DU_REPOSITORY]
   cd [NOM_DU_DOSSIER]
   ```

2. **Créer la base de données**
   
   Dans Visual Studio, ouvrir la Console du Gestionnaire de Package :
   - Aller dans **Outils** > **Gestionnaire de package NuGet** > **Console du Gestionnaire de package**
   
   Créer une migration :
   ```
   Add-Migration InitialCreate
   ```
   
   Mettre à jour la base de données :
   ```
   Update-Database
   ```

3. **Lancer l'application**
   - Appuyer sur **F5** ou cliquer sur le bouton **Démarrer** dans Visual Studio

## Structure du projet

```
Brief-Bibliotheque/
├── Connected Services/   # Services connectés
├── Dependencies/         # Dépendances NuGet
├── Properties/          # Propriétés du projet
├── wwwroot/             # Fichiers statiques (CSS, JS, images)
├── Controllers/         # Contrôleurs MVC/API
├── Handlers/            # Gestionnaire de hachage des mots de passe
├── Migrations/          # Migrations Entity Framework
├── Models/              # Modèles de données
├── Services/            # Services métier
├── Views/               # Vues Razor
├── appsettings.json     # Configuration de l'application
├── biblio.db            # Base de données SQLite
└── Program.cs           # Point d'entrée de l'application
```

---

*Développé en utilisant ASP.NET Core*