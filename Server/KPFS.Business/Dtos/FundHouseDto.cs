using KPFS.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace KPFS.Business.Dtos
{
    public class FundHouseDto : DtoBase
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
