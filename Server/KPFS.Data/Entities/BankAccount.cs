using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class BankAccount : EditEntityBase<int>
    {
        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Purpose { get; set; }

        [Required]
        public int FundId { get; set; }

        [ForeignKey(nameof(FundId))]
        public virtual Fund Fund { get; set; }
    }
}
