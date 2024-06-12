using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;

namespace BookProject.Pages.SeriesMaster
{
    public class DeleteModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public DeleteModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookSeries BookSeries { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookseries = await _context.bookSeries.Include(b => b.Authors).FirstOrDefaultAsync(m => m.Id == id);

            if (bookseries == null)
            {
                return NotFound();
            }
            else
            {
                BookSeries = bookseries;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookseries = await _context.bookSeries.FindAsync(id);
            if (bookseries != null)
            {
                BookSeries = bookseries;
                _context.bookSeries.Remove(BookSeries);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
