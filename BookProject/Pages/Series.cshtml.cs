using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Pages
{
    public class SeriesModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public BookSeries Series { get; set; } = default!;

        public SeriesModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var series = await _context.bookSeries
                .Include(b => b.Authors)
                .Include(b => b.Books)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (series == null)
            {
                return NotFound();
            }
            else
            {
                series.Books = series.Books.OrderBy(book => book.BookNumInSeries).ToList();
                Series = series;
            }

            return Page();
        }

    }
}
