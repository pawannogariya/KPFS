using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class Fund : EntityBase<int>
    {
        public int FundHouseId { get; set; }

        [StringLength(100)]

        public string Name { get; set; }

        [ForeignKey(nameof(FundHouseId))]
        public virtual FundHouse FundHouse { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
