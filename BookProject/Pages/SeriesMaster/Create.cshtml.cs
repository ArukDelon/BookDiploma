using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookProject.Models;
using BookProject.Services;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Pages.SeriesMaster
{
    public class CreateModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public CreateModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            int excludedAuthorId = 1004;

            // Використовуйте LINQ для фільтрації авторів
            var authors = await _context.authors
                                        .Where(a => a.Id != excludedAuthorId)
                                        .ToListAsync();

            // Створіть SelectList, виключаючи конкретного автора
            ViewData["AuthorsId"] = new SelectList(authors, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public BookSeries BookSeries { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.bookSeries.Add(BookSeries);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
