using System.ComponentModel.DataAnnotations;

namespace UserCRUD.Domain.Entities.Common;

public interface IEntityBase<TKey>
{
	[Key]
	public TKey Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }
	public string? CreatedBy { get; set; }
	public string? ModifiedBy { get; set; }
	public bool IsDeleted { get; set; }
}