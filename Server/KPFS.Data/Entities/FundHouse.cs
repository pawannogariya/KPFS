using KPFS.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace KPFS.Data.Entities
{
    public class FundHouse : EditEntityBase<int>
    {
        [StringLength(50)]
        public string ShortName { get; set; }


        [StringLength(100)]
        public string FullName { get; set; }    
    }
}
