using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class Investor : EntityBase<int>
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }


        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

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
