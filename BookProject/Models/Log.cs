using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string ActionType { get; set; } // Тип дії, наприклад "Додано книгу", "Видалено автора" і т.д.

        [Required]
        [StringLength(255)]
        public string Description { get; set; } // Опис дії

        public int? BookId { get; set; }
        public int? AuthorsId { get; set; } 
        public int? TagId { get; set; }
        public int? CategoryId { get; set; }
        public int? BookSeriesId { get; set; }

        [Required]
        public string AppUserId { get; set; } // Ідентифікатор адміністратора, який виконав дію

        public virtual AppUser AppUser { get; set; }
        public virtual Authors Authors { get; set; }
        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }
        public virtual BookSeries BookSeries { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
