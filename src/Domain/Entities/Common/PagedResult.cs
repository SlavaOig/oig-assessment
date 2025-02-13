namespace oig_assessment.Domain.Entities;

public class PagedResult<T>
{
    public List<T> Items { get; }
    public int Count { get; }
    public int PageNumber { get; }
    public int PageSize { get; }

    public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        Count = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
