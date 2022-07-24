using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetMongo.Requests;

public class UsertSearchRequest
{
    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public enum OrderBy
    {
        [Description("full_name")]
        FullName,
        [Description("email")]
        Email
    }

    public string Query { get; set; } = string.Empty;

    public OrderBy OrderByField { get; set; } = OrderBy.Email;
    public SortOrder Order { get; set; } = SortOrder.Ascending;

    [Range(1,int.MaxValue)]
    public int PageSize { get; set; } = 50;
    [Range(1,int.MaxValue)]
    public int PageNumber { get; set; } = 1;
}