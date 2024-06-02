namespace UserCRUD.Application.Models.Common;

public class Paginate<TItems>
{
    public TItems Items { get; set; } = default!;
    public int TotalCount { get; set; }
    public int FirstPage { get; set; }
    public int LastPage { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNext => CurrentPage < LastPage;
    public bool HasPrevious { get; set; }
}
