using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class BookSeries
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        public int AuthorsId { get; set; }

        [Display(Name = "Автор")]
        [ValidateNever]
        public virtual Authors Authors { get; set; }
        [ValidateNever]
        public virtual ICollection<Book> Books { get; set; }



    }
}
