using BookProject.Models;
using BookProject.Services;
using Google.Apis.Storage.v1;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using System.Text;
using System.Web;


namespace BookProject.Pages
{
    public class ReadBookModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private const int PageSize = 100; // Кількість символів на сторінку
        private readonly FirebaseStorageService _storageService;

        public ReadBookModel(BookProject.Services.ApplicationDbContext context, IWebHostEnvironment env, FirebaseStorageService storageService)
        {
            _context = context;
            _env = env;
            _storageService = storageService;
        }
        public Book Book { get; set; } // Модель книги для відображення в представленні
        public List<int> Pages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string BookContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (pageNumber == null)
            {
                pageNumber = 1;
            }

            var book = await _context.books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }

            string content;
            try
            {
                content = await _storageService.GetFileContentFromUrlAsync(Book.BookFilePath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Розрахунок загальної кількості сторінок
            TotalPages = (int)Math.Ceiling((double)lines.Length / PageSize);

            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > TotalPages) pageNumber = TotalPages;
            CurrentPage = (int)pageNumber;

            // Розрахунок загальної кількості сторінок
            TotalPages = (int)Math.Ceiling((double)lines.Length / PageSize);

            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > TotalPages) pageNumber = TotalPages;
            CurrentPage = (int)pageNumber;

            // Визначення початкового і кінцевого індексу рядків для поточної сторінки
            int startIndex = (CurrentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, lines.Length);

            // Отримання вмісту книги для поточної сторінки
            BookContent = string.Join("\n", lines.Skip(startIndex).Take(endIndex - startIndex));

            
            Pages = new List<int>();

            int startPage = Math.Max(1, CurrentPage - 1); // Від початку до поточної сторінки - 2
            int endPage = Math.Min(TotalPages, CurrentPage + 1); // Від поточної сторінки + 2 до останньої

            if (TotalPages > 4)
            {
                Pages.Add(1);
                Pages.Add(2);

                for (int i = startPage; i <= endPage; i++)
                {
                    if(!Pages.Contains(i))
                    {
                        Pages.Add(i);
                    }
                }

                Pages.Add(TotalPages-1);
                Pages.Add(TotalPages);
            }
            else
            {
                for (int i = 1; i <= 4; i++)
                {
                    Pages.Add(i);
                }
            }
            Pages = Pages.Distinct().OrderBy(x => x).ToList();

            return Page();
        }

    }
}
