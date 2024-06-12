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

namespace BookProject.Pages.AuthorsMaster
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(BookProject.Services.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Authors> Authors { get;set; } = default!;
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            int excludedAuthorId = 1004;
            IQueryable<Authors> authorsIQ = from s in _context.authors
                                            select s;
            var pageSize = Configuration.GetValue("PageSize", 10);
            Authors = await PaginatedList<Authors>.CreateAsync(
               authorsIQ.AsNoTracking().Where(a => a.Id != excludedAuthorId), pageIndex ?? 1, pageSize);
        }
    }
}
