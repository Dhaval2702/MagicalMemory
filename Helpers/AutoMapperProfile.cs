using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Children;
using WebApi.Models.Country;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<Account, AuthenticateResponse>();
            CreateMap<RegisterRequest, Account>();
            CreateMap<Countries, CountryResponse>();
            CreateMap<states, StateResponse>();
            CreateMap<CountryRequest, Countries>();
            CreateMap<ChildrenPaymentHistoryRequest, ChildPaymentHistory>();
            CreateMap<Children, ChildrenResponse>();
            CreateMap<ChildPaymentHistory, ChildrenPaymentHistoryResponse>();
            CreateMap<ChildrenRequest, Children>()
              .ForMember(dest => dest.ChildSkill, opt => opt.MapFrom(src => src.ChildSkillRequest))
              .ForMember(dest => dest.ChildMemory, opt => opt.MapFrom(src => src.ChildMemoryRequest))
              .ForMember(dest => dest.ChildPaymentHistory, opt => opt.MapFrom(src => src.ChildrenPaymentHistoryRequest));

            CreateMap<Children, ChildrenResponse>()
                         .ForMember(dest => dest.ChildSkillResponse, opt => opt.MapFrom(src => src.ChildSkill))
                       .ForMember(dest => dest.ChildMemoryResponse, opt => opt.MapFrom(src => src.ChildMemory))
                       .ForMember(dest => dest.ChildrenPaymentHistoryResponse, opt => opt.MapFrom(src => src.ChildPaymentHistory));

            CreateMap<ChildMemory, ChildMemoryResponse>();
            CreateMap<ChildMemoryRequest, ChildMemory>();
            CreateMap<ChildSkill, ChildSkillResponse>();
            CreateMap<ChildSkillRequest, ChildSkill>();
            CreateMap<UpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));
        }
    }
}