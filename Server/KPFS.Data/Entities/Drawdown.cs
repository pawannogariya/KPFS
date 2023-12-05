using KPFS.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPFS.Data.Entities
{
    public class Drawdown : EntityBase<int>
    {
        [Required]
        [StringLength(15)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime NoticeDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Method { get; set; }

        [Required]
        [StringLength(1000)]
        public string Notes { get; set; }

        [Required]
        public int FundId { get; set; }

        [Required]
        public int ClosureId { get; set; }

        [ForeignKey(nameof(FundId))]
        public virtual Fund Fund { get; set; }

        [ForeignKey(nameof(ClosureId))]
        public virtual Closure Closure { get; set; }
    }
}
