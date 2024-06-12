using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookProject.Models
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Повне ім'я")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Ім'я англійською")]
        [Required]
        public string FullNameEnglish { get; set; }

        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Місце народження")]
        public string BirthPlace { get; set; }

        [StringLength(10000), Display(Name = "Додаткова інформація"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ValidateNever]
        [Display(Name = "Аватар")]
        public string AvatarPath { get; set; }

        [ValidateNever]
        [NotMapped]
        [Display(Name = "Аватар")]
        public IFormFile Avatar { get; set; }
    }
}
