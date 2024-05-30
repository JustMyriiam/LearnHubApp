using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PFA.Models
{
    public class Formation
    {
        [Key]
        public int IdFormation { get; set; }

        [Required(ErrorMessage = "Le nom de la formation est requis.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Le nom de la formation doit avoir entre 10 et 100 caractères.")]
        public string? NomFormation { get; set; }

        [Required(ErrorMessage = "La durée de la formation est requise.")]
        [RegularExpression(@"^\d+ (mois|semaines)$", ErrorMessage = "La durée de la formation doit être spécifiée en mois ou en semaines (par exemple, '3 mois' ou '2 semaines').")]
        public string? Duree { get; set; }

        [Required(ErrorMessage = "La description de la formation est requise.")]
        public string? Description { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Le prix de la formation doit être un nombre positif.")]
        public float Prix { get; set; }

        public string? ImgFormationPath { get; set; }

        [Required]
        [ForeignKey("Categorie")]
        public int IdCategorie { get; set; }

        public virtual Categorie? Categorie { get; set; }

        [Required(ErrorMessage = "Le formateur associé est requis.")]
        [ForeignKey("Formateur")]
        public int IdFormateur { get; set; }

        public virtual Formateur? Formateur { get; set; }


        public virtual ICollection<Module>? Modules { get; set; } = new List<Module>();


    }
}
