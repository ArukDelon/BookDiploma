using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;

namespace BookProject.Pages.SeriesMaster
{
    public class EditModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public EditModel(BookProject.Services.ApplicationDbContext context)
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

            var bookseries =  await _context.bookSeries.FirstOrDefaultAsync(m => m.Id == id);
            if (bookseries == null)
            {
                return NotFound();
            }
            BookSeries = bookseries;

            int excludedAuthorId = 1004;

            // Використовуйте LINQ для фільтрації авторів
            var authors = await _context.authors
                                        .Where(a => a.Id != excludedAuthorId)
                                        .ToListAsync();

            // Створіть SelectList, виключаючи конкретного автора
            ViewData["AuthorsId"] = new SelectList(authors, "Id", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookSeries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookSeriesExists(BookSeries.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookSeriesExists(int id)
        {
            return _context.bookSeries.Any(e => e.Id == id);
        }
    }
}
