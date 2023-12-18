using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class FundInvestor : EntityBase<int>
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FundId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(FundId))]
        public virtual Fund Fund { get; set; }
    }
}
