using AutoMapper;
using KPFS.Business.Dtos;
using KPFS.Business.Models;
using KPFS.Data.Entities;

namespace KPFS.Web
{
    public class Mappings:Profile
    {
        public Mappings() {
            CreateMap<User, UserDto>();
            CreateMap<FundHouse, FundHouseDto>();
            CreateMap<Fund, FundDto>();
            CreateMap<BankAccount, BankAccountDto>();
            CreateMap<FundManager, FundManagerDto>();
        }
    }
}
