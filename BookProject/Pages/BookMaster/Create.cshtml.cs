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
using Microsoft.AspNetCore.Identity;
using Google.Apis.Storage.v1;

namespace BookProject.Pages.BookMaster
{
    public class CreateModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly FirebaseStorageService _storageService;
        public CreateModel(BookProject.Services.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager, FirebaseStorageService storageService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _storageService = storageService;
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
            ViewData["CategoryId"] = new SelectList(_context.category, "Id", "Name");
            ViewData["BookTags"] = new SelectList(_context.Tag, "Id", "Name");
            ViewData["SelectedTags"] = new SelectList(_context.Tag, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Book.AvatarPath = "/uploads/";
            Book.BookFilePath = "/bookfiles/";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Book.Avatar != null && Book.Avatar.Length > 0)
            {
                var photoUri = await _storageService.UploadFile(Book.Name, Book.Avatar);

                Book.AvatarPath = photoUri.ToString();
            }
            else
            {
                return Page();
            }

            if (Book.BookFile != null && Book.BookFile.Length > 0)
            {
                var bookUri = await _storageService.UploadFile(Book.Name, Book.BookFile);

                Book.BookFilePath = bookUri.ToString();
            }
            _context.books.Add(Book);

            await _context.SaveChangesAsync();

            if (SelectedTags != null)
            {
                Book.BookTags = new List<BookTag>();
                // Додаємо нові теги до списку
                foreach (var tagId in SelectedTags)
                {
                    _context.bookTags.Add(new BookTag { BookId = Book.BookId, TagId = tagId });
                }
            }

            await _context.SaveChangesAsync();

            var user = await _userManager.GetUserAsync(User);
            var author = await _context.authors
                .Where(bt => bt.Id == Book.AuthorsId)
                .FirstOrDefaultAsync();
            if (user != null && author != null)
            {
                Logger.LogAction("CREATE", " Book:'" + Book.Name + " - " + author.FullName + "' has been added to the database", adminId: user.Id, bookId: Book.BookId);
            }

            return RedirectToPage("./Index");
        }
    }
}
