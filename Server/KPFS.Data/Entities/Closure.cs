using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class Closure : EntityBase<int>
    {
        [Required]
        [StringLength(15)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        public int FundId { get; set; }

        [ForeignKey(nameof(FundId))]
        public virtual Fund Fund { get; set; }
    }
}
