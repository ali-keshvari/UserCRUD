using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.Common;

public class Base_Paging_Dto<TKey>
{
    public Guid? Id { get; set; } = null;
    public List<string>? OrderByList { get; set; } = null;
	public OrderTypeEnum? OrderType { get; set; } = OrderTypeEnum.Ascending;
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 5;

    private int _pageNum = 1;
    public int PageNum
    {
        set
        {
            _pageNum = value;
            Offset = (value - 1) * Limit;
        }
        get => _pageNum;
    }

    public string? SearchKey { get; set; } = null;
}