namespace UserCRUD.Application.Models.Common;

public class Base_List_Dto<T> where T : class
{
    public List<T> Rows { get; set; } = new();
    public int TotalCount { get; set; }
    public int FirstPage { get; set; }
    public int LastPage { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
}