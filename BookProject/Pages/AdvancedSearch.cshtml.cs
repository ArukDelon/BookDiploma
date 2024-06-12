using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookProject.Pages
{
    public class AdvancedSearchModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public AdvancedSearchModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Book> Books { get; set; } = default!;
        public int TotalTags { get; set; } = default!;

        [FromQuery(Name = "tagId")]
        public int[] SelectedTags { get; set; } = default!;

        [FromQuery(Name = "view")]
        public string ContentView { get; set; } = default!;

        [FromQuery(Name = "minPrice")]
        public decimal MinPrice { get; set; } = 10;

        [FromQuery(Name = "maxPrice")]
        public decimal MaxPrice { get; set; } = 10000;

        public string CurrentFilter { get; set; } = String.Empty;

        public string SortOrder { get; set; } = String.Empty;

        public async Task OnGetAsync(string searchString, string sortOrder)
        {
            CurrentFilter = searchString;
            SortOrder = sortOrder;

            IQueryable<Book> bookIQ = from s in _context.books
                                      select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookIQ = bookIQ
                    .Include(b => b.Authors)
                    .Include(b => b.Comments)
                    .Where(s => s.Name.Contains(searchString)
                                        || s.Authors.FullName.Contains(searchString));
            }

            if (SelectedTags != null && SelectedTags.Any())
            {
                // Фільтруємо книги за обраними тегами
                foreach (var tagId in SelectedTags)
                {
                    bookIQ = bookIQ.Where(b => b.BookTags.Any(t => t.TagId == tagId));
                }
            }

            switch (sortOrder)
            {
                case "name":
                    bookIQ = bookIQ.OrderBy(b => b.Name);
                    break;
                case "views":
                    bookIQ = bookIQ.OrderByDescending(b => b.ViewCount);
                    break;
                case "uploaddate":
                    bookIQ = bookIQ.OrderByDescending(b => b.Uploaded);
                    break;
                case "date":
                    bookIQ = bookIQ.OrderByDescending(b => b.Created);
                    break;
                case "rating":
                    bookIQ = bookIQ.OrderByDescending(b => b.AverageGrade);
                    break;
                case "comments":
                    bookIQ = bookIQ.OrderByDescending(b => b.Comments.Count);
                    break;
                default:
                    bookIQ = bookIQ.OrderBy(b => b.Name);
                    break;
            }

            if (MinPrice != 10)
            {
                bookIQ = bookIQ.Where(b => b.Price >= MinPrice);
            }

            if (MaxPrice != 10000)
            {
                bookIQ = bookIQ.Where(b => b.Price <= MaxPrice);
            }


            Books = await bookIQ
                .Include(b => b.Authors)
                .Include(b => b.Category)
                .Include(b => b.BookTags)
                .ThenInclude(bt => bt.Tag)
                .AsNoTracking().ToListAsync();

            ViewData["BookTags"] = new SelectList(_context.Tag, "Id", "Name", SelectedTags);
            ViewData["SortOrder"] = sortOrder;

            TotalTags = _context.Tag.Count();
        }

    }
}
