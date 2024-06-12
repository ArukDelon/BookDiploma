using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookProject.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Display(Name = "Назва книги")]
        public string Name { get; set; }

        [Display(Name = "Переклад")]
        public string Description { get; set; }

        [Display(Name = "Ціна")]
        [ValidateNever]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Price { get; set; }

        [Display(Name = "Жанр")]
        public int CategoryId { get; set; }

        [Display(Name = "Автор")]
        public int AuthorsId { get; set; }

        [Display(Name = "Серія книги")]
        public int? BookSeriesId { get; set; }
        [Display(Name = "Номер в серії")]
        public int? BookNumInSeries { get; set; }

        [Display(Name = "Опис")]
        public string Body { get; set; }
        [Display(Name = "Мова")]
        public string? Language { get; set; }
        [Display(Name = "Наявність")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Кількість переглядів")]
        public int ViewCount { get; set; } = 0;
        [Display(Name = "Середня оцінка")]
        public decimal AverageGrade { get; set; } = 0;
        [Display(Name = "Кількість оцінок")]
        public int TotalGrades { get; set; } = int.MinValue;
        [Display(Name = "Дата написання")]
        public DateTime Created { get; set; }
        [Display(Name = "Дата публікації")]
        public DateTime Uploaded { get; set; }

        [Display(Name = "Файл книги")]
        [ValidateNever]
        public string BookFilePath { get; set; } // Шлях до файлу книги на сервері

        [ValidateNever]
        [NotMapped]

        public IFormFile? BookFile { get; set; } // Для отримання файлу книги в запиті

        [Display(Name = "Аватар")]
        [ValidateNever]
        public string AvatarPath { get; set; }

        [ValidateNever]
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        [Display(Name = "Жанр")]
        [ValidateNever]
        public virtual Category Category { get; set; }
        [Display(Name = "Автор")]
        [ValidateNever]
        public virtual Authors Authors { get; set; }
        [Display(Name = "Серія")]
        [ValidateNever]
        public virtual BookSeries BookSeries { get; set; }

        [ValidateNever]
        public virtual ICollection<BookTag> BookTags { get; set; }
        [ValidateNever]
        public virtual ICollection<BookGrade> BookGraids { get; set; }
        [ValidateNever]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
