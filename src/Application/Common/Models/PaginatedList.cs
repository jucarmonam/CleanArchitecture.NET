using Microsoft.EntityFrameworkCore;

namespace Application.Common.Models;
public class PaginatedList<T>
{
    public IEnumerable<T> Items { get; }
    public int Page { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;

    public PaginatedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TotalCount = totalCount;
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, pageNumber, pageSize, totalCount);
    }
}
