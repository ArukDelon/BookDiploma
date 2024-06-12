using BookProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using static BookProject.Services.ApplicationDbContext;

namespace BookProject.Services
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Authors> authors { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<BookGrade> bookGrades { get; set; }
        public DbSet<BookSeries> bookSeries { get; set; }
        public DbSet<BookTag> bookTags { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<LoginHistory> loginHistories { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentItem> paymentItems { get; set; }

        public DbSet<Log> logs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            builder.Entity<IdentityRole>().HasData(admin, user);
            builder.Entity<WeeklyLoginCount>().HasNoKey().ToView(null);
            builder.Entity<TagUsage>().HasNoKey().ToView(null);
            builder.Entity<CategoryView>().HasNoKey().ToView(null);
        }
        public DbSet<BookProject.Models.Tag> Tag { get; set; } = default!;
        public DbSet<WeeklyLoginCount> WeeklyLoginCounts { get; set; }
        public DbSet<TagUsage> TagUsages { get; set; }
        public DbSet<CategoryView> CategoryViews { get; set; }

        public IQueryable<WeeklyLoginCount> GetWeeklyLoginCounts()
        {
            return this.WeeklyLoginCounts.FromSqlRaw("SELECT * FROM [dbo].[GetWeeklyLoginCounts]()");
        }
        public IQueryable<TagUsage> GetTopTagUsages()
        {
            return this.TagUsages.FromSqlRaw("SELECT * FROM [dbo].[GetTopTagUsage]()");
        }
        public IQueryable<CategoryView> GetCategoryViews()
        {
            return this.CategoryViews.FromSqlRaw("SELECT * FROM [dbo].[GetCategoryViewCounts]()");
        }


        [NotMapped]
        public class TagUsage
        {
            public string TagName { get; set; }
            public int UsageCount { get; set; }
        }


        [NotMapped]
        public class WeeklyLoginCount
        {
            public DateTime LoginDate { get; set; }
            public int LoginCount { get; set; }
        }

        [NotMapped]
        public class CategoryView
        {
            public string CategoryName { get; set; }
            public int ViewCount { get; set; }
        }
    }
}
    