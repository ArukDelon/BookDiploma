using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Pages.AuthorsMaster
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DeleteModel(BookProject.Services.ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Authors Authors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.authors.FirstOrDefaultAsync(m => m.Id == id);

            if (authors == null)
            {
                return NotFound();
            }
            else
            {
                Authors = authors;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.authors.FindAsync(id);
            if (authors != null)
            {
                var logs = _context.logs.Where(log => log.AuthorsId == id).ToList();
                foreach (var log in logs)
                {
                    log.AuthorsId = 1004;
                }
                var booksWithAuthor = _context.books.Where(b => b.AuthorsId == id).ToList();
                foreach (var book in booksWithAuthor)
                {
                    book.AuthorsId = 1004;
                }

                await _context.SaveChangesAsync();

                Authors = authors;
                _context.authors.Remove(Authors);
                await _context.SaveChangesAsync();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Logger.LogAction("DELETE", " Author:'" + authors.FullName + "' has been deleted from the database", adminId: user.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
