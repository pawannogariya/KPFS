using KPFS.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPFS.Data.Entities
{
    public class Fund : EditEntityBase<int>
    {
        [Required]
        [StringLength(15)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string SebiRegistrationNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string InvestmentManagerName { get; set; }

        [Required]
        [StringLength(250)]
        public string SponserName { get; set; }

        [Required]
        [StringLength(250)]
        public string MerchantBankerName { get; set; }

        [Required]
        [StringLength(250)]
        public string LegalAdvisorName { get; set; }

        [Required]
        public int FundHouseId { get; set; }

        [ForeignKey(nameof(FundHouseId))]
        public virtual FundHouse FundHouse { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
