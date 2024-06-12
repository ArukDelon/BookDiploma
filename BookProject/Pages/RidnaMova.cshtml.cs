using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookProject.Pages
{
    public class RidnaMovaModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        public RidnaMovaModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Shevchenko { get; set; }
        public IList<Book> Ukrainka { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Book> booksIQ = from s in _context.books
                                       select s;

            var books = await booksIQ.AsNoTracking().Where(m => m.AuthorsId == 2).Include(b => b.Authors).Take(2).ToListAsync();


            if (books != null)
            {
                Shevchenko = books;
            }

            books = await booksIQ.AsNoTracking().Where(m => m.AuthorsId == 3).Include(b => b.Authors).Take(2).ToListAsync();

            if (books != null)
            {
                Ukrainka = books;
            }


            return Page();
        }
    }
}
