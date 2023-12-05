using KPFS.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace KPFS.Data.Entities
{
    public class TemporaryInvestment : EntityBase<int>
    {
        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
