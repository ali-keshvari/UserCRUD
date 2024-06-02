using UserCRUD.Domain.Entities.Common;

namespace UserCRUD.Domain.Entities.Identity;

public class User : EntityBase
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
    public string PersonalCode { get; set; }
    public string NationalCode { get; set; }
    public ICollection<Upload_File> Files { get; set; }
}