namespace PetMongo.Responses;

public  class PagedResultSet<T>
{
    public PagedResultSet(long pageNumber, long  pageSize, long totalPageCount, long totalItemsCount, IEnumerable<T> item)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPageCount = totalPageCount;
        TotalItemsCount = totalItemsCount;
        Items = item;
    }
    
    public PagedResultSet(){}

    public long PageNumber { get; set; }
    
    public long PageSize { get; set; }

    public long TotalPageCount { get; set; }
    
    public long TotalItemsCount { get; set; }
    
    public IEnumerable<T> Items { get; set; }
}