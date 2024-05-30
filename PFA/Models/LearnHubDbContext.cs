using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Models
{
    public class LearnHubDbContext : IdentityDbContext
    {
        public DbSet<Formation>? Formations { get; set; }
        public DbSet<Formateur>? Formateurs { get; set; }
        public DbSet<Categorie>? Categories { get; set; }
        public DbSet<Module>? Modules { get; set; }


        public LearnHubDbContext(DbContextOptions<LearnHubDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Formateur>().HasData(
                new Formateur { Id = 1, Nom = "john", Prenom = "doe", Email = "john.doe@example.com", Biographie = "professeur à l'université de nantes et formateur en data science depuis 2010" },
                new Formateur { Id = 2, Nom = "jane", Prenom = "smith", Email = "jane.smith@example.com", Biographie = "expert en cybersécurité." }
            );

            modelBuilder.Entity<Categorie>().HasData(
                new Categorie { IdCategorie = 1, NomCategorie = "Data Science", DescriptionCat = "catégorie de formation axée sur l'apprentissage de la science des données et de l'analyse de données." },
                new Categorie { IdCategorie = 2, NomCategorie = "Cybersécurité", DescriptionCat = "catégorie de formation axée sur la sécurité informatique et la protection des systèmes informatiques." }
            );

            modelBuilder.Entity<Formation>().HasData(
              new Formation { IdFormation = 1, NomFormation = "Formation en data science avancée", Duree = "3 mois", Description = "formation avancée en data science", Prix = 1000.0f, IdFormateur = 1, IdCategorie = 1, ImgFormationPath = "dddd" },
              new Formation { IdFormation = 2, NomFormation = "Formation en cybersécurité professionnelle", Duree = "2 mois", Description = "formation professionnelle en cybersécurité", Prix = 800.0f, IdFormateur = 2, IdCategorie = 2, ImgFormationPath = "gggg" }
            );

            modelBuilder.Entity<Module>().HasData(
               new Module
               {
                   IdModule = 1,
                   NomModule = "Introduction à la Data Science",
                   IdFormation = 1,
                   DescriptionMod = "Introduction aux concepts de base de la data science."
               },
               new Module
               {
                   IdModule = 2,
                   NomModule = "Machine Learning",
                   IdFormation = 1,
                   DescriptionMod = "Étude des algorithmes de machine learning pour l'analyse prédictive."
               },
               new Module
               {
                   IdModule = 3,
                   NomModule = "Big Data",
                   IdFormation = 1,
                   DescriptionMod = "Gestion et analyse de grands ensembles de données."
               },
               new Module
               {
                   IdModule = 4,
                   NomModule = "Sécurité des Réseaux",
                   IdFormation = 2,
                   DescriptionMod = "Principes de base de la sécurité des réseaux informatiques."
               },
               new Module
               {
                   IdModule = 5,
                   NomModule = "Cryptographie",
                   IdFormation = 2,
                   DescriptionMod = "Étude des techniques de cryptographie pour assurer la sécurité des données."
               },
               new Module
               {
                   IdModule = 6,
                   NomModule = "Gestion des Risques en Cybersécurité",
                   IdFormation = 2,
                   DescriptionMod = "Identification et gestion des risques en cybersécurité."
               }
           );

           

            base.OnModelCreating(modelBuilder);
        }

    }
}
