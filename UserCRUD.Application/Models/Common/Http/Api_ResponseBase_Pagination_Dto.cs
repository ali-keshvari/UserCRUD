namespace UserCRUD.Application.Models.Common.Http;

public class User_ResponseBase_Pagination_Dto
{
    public int TotalCount { get; set; }
    public int FirstPage { get; set; }
    public int LastPage { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
}
