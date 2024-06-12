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

namespace BookProject.Pages.TagMaster
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

        public PaginatedList<Tag> Tag { get;set; } = default!;
        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Tag> tagsIQ = from s in _context.Tag
                                               select s;

            var pageSize = Configuration.GetValue("PageSize", 10);

            Tag = await PaginatedList<Tag>.CreateAsync(
              tagsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
