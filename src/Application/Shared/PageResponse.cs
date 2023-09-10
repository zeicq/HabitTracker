namespace Application.Shared;

public class PageResponse<T>: Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PageResponse(T data, int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.Data = data;
        this.Succeeded = true;
    }
}