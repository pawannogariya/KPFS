using AutoMapper;
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
            CreateMap<Role, RoleDto>();



            CreateMap<FundHouseDto, FundHouse>();
            CreateMap<FundDto, Fund>();
            CreateMap<RegisterUserDto, User>()
               .IncludeAllDerived()
               .ForMember(dest => dest.UserName, act => act.MapFrom(x => x.Email))
               .ForMember(dest => dest.TwoFactorEnabled, act => act.MapFrom(x => true))
               .ForMember(dest => dest.IsActive, act => act.MapFrom(x => true))
               .ForMember(dest => dest.SecurityStamp, act => act.MapFrom(x => Guid.NewGuid().ToString()));
        }
    }
}
