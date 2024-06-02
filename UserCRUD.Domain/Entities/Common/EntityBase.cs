using System.ComponentModel.DataAnnotations;

namespace UserCRUD.Domain.Entities.Common;

public class EntityBase: IEntityBase<Guid>
{
	[Key]
	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }
	public string? CreatedBy { get; set; }
	public string? ModifiedBy { get; set; }
	public bool IsDeleted { get; set; }
}