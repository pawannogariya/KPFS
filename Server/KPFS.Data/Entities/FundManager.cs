using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class FundManager : EntityBase<int>
    {
        [Required]
        [StringLength(100)]
        public string ManagerFirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string ManagerLastName { get; set; }


        [Required]
        [StringLength(500)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int FundId { get; set; }

        [ForeignKey(nameof(FundId))]
        public virtual Fund Fund { get; set; }
    }
}
