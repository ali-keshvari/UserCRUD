namespace UserCRUD.Application.Models.DTOs.User;

public class User_List_Item_Dto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalCode { get; set; }
    public string NationalCode { get; set; }
}