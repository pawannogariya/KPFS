using KPFS.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPFS.Data.Entities
{
    public class Investor : EntityBase<int>
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UniqueKey { get; set; }

        [Required]
        [StringLength(100)]
        public string FolioNo { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsCarryClass { get; set; }

        [Required]
        [StringLength(100)]
        public string Class { get; set; }

        public DateTime? ForfeitAndTransferDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Salutation { get; set; }

        [Required]
        [StringLength(200)]
        public string InvestorName { get; set; }

        [Required]
        public int ModeOfHolding { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; }

        [EmailAddress]
        [StringLength(250)]
        public string Email2 { get; set; }


        [Required]
        [StringLength(20)]
        public string IdentityPanNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string TaxPanNumber { get; set; }

        [StringLength(20)]
        public string ContactNo { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(20)]
        public string Pincode { get; set; }

        [Required]
        public int BankAccountType { get; set; }

        public string? OtherBankAccountType { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [StringLength(20)]
        public string? IFSCCode { get; set; }

        [StringLength(50)]
        public string? MICRCode { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        public int TaxStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string SetupFees { get; set; }

        [Required]
        [StringLength(100)]
        public string ManagementFees { get; set; }

        [Required]
        [StringLength(100)]
        public string OperatingExpenses { get; set; }

        [Required]
        [StringLength(100)]
        public string SebiInvestorType1 { get; set; }

        [Required]
        [StringLength(100)]
        public string SebiInvestorType2 { get; set; }

        [StringLength(100)]
        public string SebiInvestorType3 { get; set; }


        [Required]
        [Range(1, double.MaxValue)]
        public double CapitalCommitment { get; set; }

        [Required]
        public double CapitalContribution { get; set; }

        [Required]
        [StringLength(100)]
        public string KpfsRecordStatus { get; set; }

        [StringLength(500)]
        public string? KpfsIncompleteRecordRemark { get; set; }

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
