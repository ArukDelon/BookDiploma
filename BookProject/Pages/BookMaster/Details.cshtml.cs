using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;

namespace BookProject.Pages.BookMaster
{
    public class DetailsModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public DetailsModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books.Include(b => b.Authors).Include(b => b.Category).FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }
    }
}
