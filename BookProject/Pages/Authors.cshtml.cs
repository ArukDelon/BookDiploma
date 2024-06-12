using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Pages
{
    public class AuthorsModel : PageModel
    {

        private readonly BookProject.Services.ApplicationDbContext _context;
        public AuthorsModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }

        public IList<Authors> Authors { get; set; } = default!;

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;

            IQueryable<Authors> authorsIQ = from s in _context.authors
                                            where s.Id != 1004
                                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                authorsIQ = authorsIQ.Where(s => s.FullName.Contains(searchString)
                                       || s.FullNameEnglish.Contains(searchString));
            }

            Authors = await authorsIQ.AsNoTracking().ToListAsync();
        }
    }
}
