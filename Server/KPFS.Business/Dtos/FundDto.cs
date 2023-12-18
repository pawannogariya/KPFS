using KPFS.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace KPFS.Business.Dtos
{
    public class FundDto : DtoBase
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string SebiRegistrationNumber { get; set; }

        [Required]
        public string InvestmentManagerName { get; set; }

        [Required]
        public string SponserName { get; set; }

        [Required]
        public string MerchantBankerName { get; set; }

        [Required]
        public string LegalAdvisorName { get; set; }

        [Required]
        public int FundHouseId { get; set; }
    }
}
