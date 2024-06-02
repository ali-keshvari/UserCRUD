using UserCRUD.Application.Models.DTOs.User;

namespace UserCRUD.Application.Contracts.Services
{
    public interface IReportService
    {
        Task<string> ExcelReport(List<User_List_Item_Dto> users);
        Task<string> PdfReport(List<User_List_Item_Dto> users);
    }
}
