using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPFS.Data.Entities.Base
{
    public class EntityBase<TId>
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }

        public bool IsNew => Id.Equals(default(TId));

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual User UpdateByUser { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public virtual User DeletedByUser { get; set; }

        public void MarkAsCreatedBy(User user)
        {
            CreatedBy = user.Id;
            CreatedOn = DateTime.UtcNow;
        }

        public void MarkAsUpdatedBy(User user)
        {
            UpdatedBy = user.Id;
            UpdatedOn = DateTime.UtcNow;
        }

        public void MarkAsDeleted(User user)
        {
            IsDeleted = true;
            DeletedBy = user.Id;
            DeletedOn = DateTime.UtcNow;
        }
    }
}
