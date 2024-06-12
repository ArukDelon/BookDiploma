using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookProject.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public CategoryModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public string SortOrder { get; set; } = String.Empty;
        public async Task<IActionResult> OnGetAsync(int? id, string sortOrder, string? cName)
        {

            SortOrder = sortOrder;

            if(id != null)
            {
                Category = await _context.category.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            }
            
            if(cName != null)
            {
                Category = await _context.category.AsNoTracking().FirstOrDefaultAsync(m => m.Name.Equals(cName));
            }

          

           IQueryable <Book> booksIQ = from s in _context.books
                                            select s;
            booksIQ = booksIQ.Where(m => m.CategoryId == Category.Id);

            switch (sortOrder)
            {
                case "name":
                    booksIQ = booksIQ.OrderBy(b => b.Name);
                    break;
                case "uploaddate":
                    booksIQ = booksIQ.OrderByDescending(b => b.Uploaded);
                    break;
                case "date":
                    booksIQ = booksIQ.OrderByDescending(b => b.Created);
                    break;
                case "rating":
                    booksIQ = booksIQ.OrderByDescending(b => b.AverageGrade);
                    break;
                case "comments":
                    booksIQ = booksIQ.OrderByDescending(b => b.Comments.Count);
                    break;
                default:
                    booksIQ = booksIQ.OrderBy(b => b.Name);
                    break;
            }

            var books = await booksIQ.AsNoTracking().Include(b => b.Authors)
                .Include(b => b.Category).ToListAsync();
            
            if (books == null)
            {
                return NotFound();
            }
            else
            {
                Books = books;
            }

            ViewData["SortOrder"] = sortOrder;

            ViewData["List"] = sortOrder;

            return Page();
        }
    }
}
