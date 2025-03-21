namespace NTTDataSales.Domain.Mappings.DTO;

public class PagedDTO<T>
{
    public IEnumerable<T> data { get; set; }
    public int totalItems { get; set; }
    public int totalPages { get; set; }
    public int currentPage { get; set; }
}
