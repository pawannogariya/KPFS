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
    }
}
