using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [ValidateNever]
        public virtual ICollection<BookTag> BookTags { get; set;}
    }
}
