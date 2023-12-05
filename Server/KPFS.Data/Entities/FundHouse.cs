using System.ComponentModel.DataAnnotations;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class FundHouse : EntityBase<int>
    {
        [StringLength(50)]
        public string ShortName { get; set; }


        [StringLength(100)]
        public string FullName { get; set; }    
    }
}
