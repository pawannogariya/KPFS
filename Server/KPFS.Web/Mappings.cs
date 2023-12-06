using AutoMapper;
using K4os.Hash.xxHash;
using KPFS.Business.Dtos;
using KPFS.Business.Models;
using KPFS.Data.Entities;

namespace KPFS.Web
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, act => act.MapFrom(x => x.UserRoles.Select(u => u.Role).First().Name));

            CreateMap<FundHouse, FundHouseDto>();
            CreateMap<Fund, FundDto>();
            CreateMap<BankAccount, BankAccountDto>();
            CreateMap<FundManager, FundManagerDto>();
        }
    }
}
