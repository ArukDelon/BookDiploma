using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Pages.BookMaster
{
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
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books.Include(b => b.Authors).Include(b => b.BookTags).FirstOrDefaultAsync(m => m.BookId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books.FindAsync(id);
            if (book != null)
            {

                var logs = _context.logs.Where(log => log.BookId == id).ToList();
                foreach (var log in logs)
                {
                    log.BookId = 1004;
                }
                await _context.SaveChangesAsync();

                Book = book;
                _context.books.Remove(Book);
                await _context.SaveChangesAsync();
            }

            var user = await _userManager.GetUserAsync(User);
            var author = await _context.authors
                .Where(bt => bt.Id == Book.AuthorsId)
                .FirstOrDefaultAsync();
            if (user != null && author != null)
            {
                Logger.LogAction("DELETE", " Book:'" + Book.Name + " - " + author.FullName + "' has been deleted from the database", adminId: user.Id, bookId: Book.BookId);
            }

            return RedirectToPage("./Index");
        }
    }
}
