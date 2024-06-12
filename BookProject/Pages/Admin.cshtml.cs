using BookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static BookProject.Services.ApplicationDbContext;

namespace BookProject.Pages
{

    [Authorize(Roles = "admin")]
    public class AdminModel : PageModel
    {
        private readonly Services.ApplicationDbContext _context;

        public AdminModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Log> Logs { get; set; }

        public async Task OnGetAsync()
        {
            var recentLogs = await _context.logs.OrderByDescending(log => log.ActionDate).Take(6).ToListAsync();
            if(recentLogs.Count > 0)
            {
                Logs = recentLogs;
            }
        }

        public List<WeeklyLoginCount> GetWeeklyLoginCounts()
        {
            return _context.GetWeeklyLoginCounts().ToList();
        }
        public List<TagUsage> GetTagUsages()
        {
            return _context.GetTopTagUsages().ToList();
        }

        public List<CategoryView> GetCategoryViews()
        {
            return _context.GetCategoryViews().ToList();
        }
    }
}
