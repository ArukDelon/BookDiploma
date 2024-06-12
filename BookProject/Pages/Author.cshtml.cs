using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Pages
{
    public class AuthorModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public AuthorModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Authors Author { get; set; } = default!;
        public IList<Book> Books { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.authors.FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            else
            {
                Author = author;
            }

            IQueryable<Book> booksIQ = from s in _context.books
                                       select s;
            booksIQ = booksIQ.Where(m => m.AuthorsId == id);

            var books = await booksIQ.AsNoTracking().Include(b => b.Authors).ToListAsync();

            Books = books;


            return Page();
        }
    }
}
