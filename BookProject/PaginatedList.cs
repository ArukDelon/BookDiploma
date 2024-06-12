using Microsoft.EntityFrameworkCore;

namespace BookProject
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; } // Номер сторінки
        public int TotalPages { get; private set; } // Загальна кількість сторінок

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) // Конструктор
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync( 
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
