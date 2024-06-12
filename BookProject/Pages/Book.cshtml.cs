using BookProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookProject.Pages
{
    public class BookModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private const int CommentsPerPage = 4;

        public BookModel(BookProject.Services.ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Book Book { get; set; } = default!;


        [BindNever]
        public decimal UserGrade { get; set; } = default!;
        public bool IsPurchased { get; set; } = false;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public string CommentText { get; set; }

        public IList<Tag> _bookTags { get; set; } = default!;
        public IList<Comment> Comments { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id, int pageIndex = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books
                .Include(b => b.Authors)
                .Include(b => b.Category)
                .Include(b => b.Comments)
                .Include(b => b.BookSeries)
                .Include(b => b.BookTags)
                    .ThenInclude(bt => bt.Tag)
                .Include(b => b.BookGraids)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }



            _bookTags = Book.BookTags?.Select(bt => bt.Tag).ToList() ?? new List<Tag>();

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userGrade = await _context.bookGrades
                    .FirstOrDefaultAsync(bg => bg.BookId == book.BookId && bg.AppUserId == user.Id);

                var purch = await _context.payments.Include(p => p.PaymentItems).FirstOrDefaultAsync(bg => bg.AppUserId == user.Id && bg.PaymentItems.Any(pi => pi.BookId == book.BookId));

                if (userGrade != null)
                {
                    UserGrade = userGrade.Grade;
                }
                else
                {
                    UserGrade = Book.AverageGrade;
                }

                if (purch != null)
                {
                    IsPurchased = true;
                }
                else
                {
                    IsPurchased = false;
                }
            }


            Book.TotalGrades = Book.BookGraids.Count;
            if (Book.TotalGrades > 0)
            {
                Book.AverageGrade = Book.BookGraids.Sum(bg => bg.Grade) / Book.TotalGrades;
            }
            else
            {
                Book.AverageGrade = 0;
            }
            // Пагінація для коментарів
            var totalComments = Book.Comments.Count;
            TotalPages = (int)Math.Ceiling(totalComments / (double)CommentsPerPage);
            CurrentPage = pageIndex;

            var commentsQuery = _context.comments
                .Where(c => c.BookId == id)
                .Include(bc => bc.AppUser)
                .OrderByDescending(c => c.CreatedDate);

            Comments = await commentsQuery
                .Skip((pageIndex - 1) * CommentsPerPage)
                .Take(CommentsPerPage)
                .ToListAsync();

            Book.ViewCount++;
            await _context.SaveChangesAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostGradeAsync(int UserGrade, int bookId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Book", new { id = bookId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "You need to log in to vote.";
                return RedirectToPage("/Book", new { id = bookId });
            }

            // Отримайте об'єкт книги за допомогою ID
            var bookFromDb = await _context.books
                .Include(b => b.BookGraids)
                .FirstOrDefaultAsync(m => m.BookId == bookId);

            if (bookFromDb == null)
            {
                return NotFound();
            }

            var existingGrade = bookFromDb.BookGraids.FirstOrDefault(bg => bg.AppUserId == user.Id);
            if (existingGrade != null)
            {
                TempData["ErrorMessage"] = "You have already voted for this book.";
                return RedirectToPage("/Book", new { id = bookId });
            }



            // Додайте нову оцінку до списку оцінок книги
            bookFromDb.BookGraids.Add(new BookGrade
            {
                AppUserId = user.Id, // Поточний користувач або відповідний ідентифікатор
                Grade = UserGrade // Оцінка, яку вводить користувач
            });

            // Перерахуйте середню оцінку та загальну кількість оцінок
            bookFromDb.TotalGrades = bookFromDb.BookGraids.Count;
            bookFromDb.AverageGrade = bookFromDb.BookGraids.Sum(bg => bg.Grade) / bookFromDb.TotalGrades;

            // Оновіть об'єкт книги в базі даних
            _context.books.Update(bookFromDb);
            await _context.SaveChangesAsync();


            return RedirectToPage("/Book", new { id = bookId });
        }

        public async Task<IActionResult> OnPostAsync(int bookId, string commentBody)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Book", new { id = bookId });
            }

            if (string.IsNullOrWhiteSpace(commentBody))
            {
                ModelState.AddModelError(string.Empty, "Коментар не може бути порожнім.");
                return await OnGetAsync(bookId); // Перезавантажити сторінку з поточними даними
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Вам потрібно увійти, щоб залишити коментар.";
                return RedirectToPage("/Account/Login");
            }

            var book = await _context.books
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(m => m.BookId == bookId);

            if (book == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                AppUserId = user.Id,
                BookId = book.BookId,
                Body = commentBody,
                CreatedDate = DateTime.Now
            };

            book.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Book", new { id = bookId });
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var book = await _context.books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var cartItems = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            var existingItem = cartItems.Items.FirstOrDefault(i => i.BookId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newCartItem = new CartItem { BookId = book.BookId, Book = book, Quantity = 1 };
                cartItems.Items.Add(newCartItem);
            }

            HttpContext.Session.SetObjectAsJson("Cart", cartItems);

            return RedirectToPage("/Cart");
        }

    }
}

