using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace BookProject.Pages.CategoryMaster
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

        public PaginatedList<Category> Category { get; set; } = default!;
        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Category> categoryIQ = from s in _context.category
                                            select s;
            var pageSize = Configuration.GetValue("PageSize", 10);
            Category = await PaginatedList<Category>.CreateAsync(
               categoryIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
