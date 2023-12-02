using System.ComponentModel.DataAnnotations;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class FundHouse : EntityBase<int>
    {
        [StringLength(100)]

        public string Name { get; set; }    
    }
}
