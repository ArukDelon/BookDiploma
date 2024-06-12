using BookProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookProject.Services
{
    public static class Logger
    {
        private static IServiceProvider _serviceProvider;

        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static void LogAction(string actionType, string description, string adminId, 
            int? bookId = null, int? authorId = null, int? tagId = null, int? categoryId = null, int? bookSeriesId = null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var log = new Log
                {
                    ActionType = actionType,
                    Description = description,
                    AppUserId = adminId,
                    BookId = bookId,
                    AuthorsId = authorId,
                    TagId = tagId,
                    CategoryId = categoryId,
                    BookSeriesId = bookSeriesId
                };

                _context.logs.Add(log);
                _context.SaveChanges();
            }
        }
    }
}
