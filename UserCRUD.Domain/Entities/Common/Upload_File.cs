using UserCRUD.Domain.Entities.Identity;

namespace UserCRUD.Domain.Entities.Common
{
    public class Upload_File : EntityBase
    {
        public string Path{ get; set; }
        public virtual User user { get; set; }
        public Guid UserId{ get; set; }
        public string FileName { get; set; }
    }
}
