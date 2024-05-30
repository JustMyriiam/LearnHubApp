using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomCategorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionCat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategorie);
                });

            migrationBuilder.CreateTable(
                name: "Formateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biographie = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    IdFormation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFormation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Duree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false),
                    ImgFormationPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCategorie = table.Column<int>(type: "int", nullable: false),
                    IdFormateur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.IdFormation);
                    table.ForeignKey(
                        name: "FK_Formations_Categories_IdCategorie",
                        column: x => x.IdCategorie,
                        principalTable: "Categories",
                        principalColumn: "IdCategorie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Formations_Formateurs_IdFormateur",
                        column: x => x.IdFormateur,
                        principalTable: "Formateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    IdModule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomModule = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionMod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdFormation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.IdModule);
                    table.ForeignKey(
                        name: "FK_Modules_Formations_IdFormation",
                        column: x => x.IdFormation,
                        principalTable: "Formations",
                        principalColumn: "IdFormation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategorie", "DescriptionCat", "NomCategorie" },
                values: new object[,]
                {
                    { 1, "catégorie de formation axée sur l'apprentissage de la science des données et de l'analyse de données.", "Data Science" },
                    { 2, "catégorie de formation axée sur la sécurité informatique et la protection des systèmes informatiques.", "Cybersécurité" }
                });

            migrationBuilder.InsertData(
                table: "Formateurs",
                columns: new[] { "Id", "Biographie", "DateNaissance", "Email", "Nom", "NumTel", "Prenom" },
                values: new object[,]
                {
                    { 1, "professeur à l'université de nantes et formateur en data science depuis 2010", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "john", null, "doe" },
                    { 2, "expert en cybersécurité.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "jane", null, "smith" }
                });

            migrationBuilder.InsertData(
                table: "Formations",
                columns: new[] { "IdFormation", "Description", "Duree", "IdCategorie", "IdFormateur", "ImgFormationPath", "NomFormation", "Prix" },
                values: new object[] { 1, "formation avancée en data science", "3 mois", 1, 1, "dddd", "Formation en data science avancée", 1000f });

            migrationBuilder.InsertData(
                table: "Formations",
                columns: new[] { "IdFormation", "Description", "Duree", "IdCategorie", "IdFormateur", "ImgFormationPath", "NomFormation", "Prix" },
                values: new object[] { 2, "formation professionnelle en cybersécurité", "2 mois", 2, 2, "gggg", "Formation en cybersécurité professionnelle", 800f });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "IdModule", "DescriptionMod", "IdFormation", "NomModule" },
                values: new object[,]
                {
                    { 1, "Introduction aux concepts de base de la data science.", 1, "Introduction à la Data Science" },
                    { 2, "Étude des algorithmes de machine learning pour l'analyse prédictive.", 1, "Machine Learning" },
                    { 3, "Gestion et analyse de grands ensembles de données.", 1, "Big Data" },
                    { 4, "Principes de base de la sécurité des réseaux informatiques.", 2, "Sécurité des Réseaux" },
                    { 5, "Étude des techniques de cryptographie pour assurer la sécurité des données.", 2, "Cryptographie" },
                    { 6, "Identification et gestion des risques en cybersécurité.", 2, "Gestion des Risques en Cybersécurité" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formations_IdCategorie",
                table: "Formations",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Formations_IdFormateur",
                table: "Formations",
                column: "IdFormateur");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IdFormation",
                table: "Modules",
                column: "IdFormation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Formations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Formateurs");
        }
    }
}
