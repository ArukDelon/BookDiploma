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
using Microsoft.AspNetCore.Hosting;
using BookProject.Models.ViewModels;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Pages.BookMaster
{
    public class EditModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly FirebaseStorageService _storageService;
        public EditModel(BookProject.Services.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager, FirebaseStorageService storageService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _storageService = storageService;
        }

        [BindProperty]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        public bool isAvatar { get; set; }

        [BindProperty]
        public bool isBookFile { get; set; }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  await _context.books.Include(b => b.Authors).Include(b => b.BookTags).FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
           ViewData["AuthorsId"] = new SelectList(_context.authors, "Id", "FullName");
           ViewData["CategoryId"] = new SelectList(_context.category, "Id", "Name");
           ViewData["BookTags"] = new SelectList(_context.Tag, "Id", "Name");

            var selectedTagIds = Book.BookTags.Select(bt => bt.TagId).ToList();
            SelectedTags = selectedTagIds;
            ViewData["SelectedTags"] = new SelectList(_context.Tag, "Id", "Name", selectedTagIds);

            var series = _context.bookSeries
               .Where(bs => bs.AuthorsId == Book.AuthorsId)
               .Select(bs => new { bs.Id, bs.Name })
               .ToList();

            series.Insert(0, new { Id = 0, Name = "Select Series" });

            ViewData["BookSeriesId"] = new SelectList(series, "Id", "Name");


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

            if(Book.BookSeriesId == 0)
            {
                Book.BookSeriesId = null;
            }

            if (Book.Avatar != null && Book.Avatar.Length > 0)
            {
                var photoUri = await _storageService.UploadFile(Book.Name, Book.Avatar);

                Book.AvatarPath = photoUri.ToString();
            }

            if (Book.BookFile != null && Book.BookFile.Length > 0)
            {
                var bookUri = await _storageService.UploadFile(Book.Name, Book.BookFile);

                Book.BookFilePath = bookUri.ToString();
            }


            if (SelectedTags != null)
            {
                Book.BookTags = new List<BookTag>();
                var existingTags = await _context.bookTags
                .Where(bt => bt.BookId == Book.BookId)
                .Select(bt => bt.TagId)
                .ToListAsync();

                // Додаємо нові теги до списку
                foreach (var tagId in SelectedTags)
                {
                    if (!existingTags.Contains(tagId))
                    {
                        // Додати новий тег до бази даних
                        _context.bookTags.Add(new BookTag { BookId = Book.BookId, TagId = tagId });
                    }
                }

                // Видаляємо теги, які були вибрані раніше, але тепер не вибрані
                foreach (var tagId in existingTags)
                {
                    if (!SelectedTags.Contains(tagId))
                    {
                        // Знаходимо тег, який потрібно видалити
                        var tagToRemove = await _context.bookTags.FirstOrDefaultAsync(bt => bt.BookId == Book.BookId && bt.TagId == tagId);
                        if (tagToRemove != null)
                        {
                            // Видаляємо тег з бази даних
                            _context.bookTags.Remove(tagToRemove);
                        }
                    }
                }


            }

            var user = await _userManager.GetUserAsync(User);
            var author = await _context.authors
                .Where(bt => bt.Id == Book.AuthorsId)
                .FirstOrDefaultAsync();
            if (user != null && author != null)
            {
                Logger.LogAction("EDIT", "Edit data for book:'" + Book.Name + " - " + author.FullName + "'", adminId: user.Id, bookId: Book.BookId);
            }

            _context.Attach(Book).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.BookId))
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

        private bool BookExists(int id)
        {
            return _context.books.Any(e => e.BookId == id);
        }

        public JsonResult OnGetSeriesByAuthor(int authorsId)
        {
            var series = _context.bookSeries
                .Where(bs => bs.AuthorsId == authorsId)
                .Select(bs => new { bs.Id, bs.Name })
                .ToList();
            return new JsonResult(new SelectList(series, "Id", "Name"));
        }
    }

}


