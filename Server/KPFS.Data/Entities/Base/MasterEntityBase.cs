using System.ComponentModel.DataAnnotations;

namespace KPFS.Data.Entities.Base
{
    public class MasterEntityBase<TId> : EntityBase<TId>
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
