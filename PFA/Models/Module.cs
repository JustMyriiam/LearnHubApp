using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PFA.Models
{
    public class Module
    {
        [Key]
        public int IdModule { get; set; }

        [Required(ErrorMessage = "Le nom du module est requis.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom du module doit avoir entre 3 et 100 caractères.")]
        public string? NomModule { get; set; }

        [Required(ErrorMessage = "La description du module est requise.")]
        [RegularExpression(@"^.{1,500}$", ErrorMessage = "La description du module doit être un paragraphe d'au maximum 500 caractères.")]
        public string? DescriptionMod { get; set; }


        [Required(ErrorMessage = "La formation associée est requise.")]
        [ForeignKey("Formation")]
        public int IdFormation { get; set; }

        public virtual Formation? Formation { get; set; }
    }
}
