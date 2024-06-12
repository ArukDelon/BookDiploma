using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BookProject.Services.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, Services.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Book> Books { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IQueryable<Book> bookIQ = from s in _context.books
                                      select s;

            bookIQ = bookIQ.OrderByDescending(b => b.Uploaded);

            Books = await bookIQ
                .Include(b => b.Authors)
                .Include(b => b.Category)
                .AsNoTracking().ToListAsync();
        }
    }
}
