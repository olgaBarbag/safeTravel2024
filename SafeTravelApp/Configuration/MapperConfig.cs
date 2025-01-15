using AutoMapper;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.User;

namespace SafeTravelApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Details!.PhoneNumber}"))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => $"{src.Details!.Country}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Details!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details!.Address}"))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => $"{src.Details!.PostalCode}"))
                .ReverseMap();
                
            CreateMap<User, UserSignupDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Details!.PhoneNumber}"))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => $"{src.Details!.Country}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Details!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details!.Address}"))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => $"{src.Details!.PostalCode}"))
                .ReverseMap();

            CreateMap<User, UserReadOnlyDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();

//---------------------------------------------------------------------------------------------------------------------------

            CreateMap<User, AgentReadOnlyDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => $"{src.Agent!.CompanyName}"))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => $"{src.Agent!.VatNumber}"))
                .ReverseMap();

            CreateMap<User, AgentSignUpDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Details!.PhoneNumber}"))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => $"{src.Details!.Country}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Details!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details!.Address}"))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => $"{src.Details!.PostalCode}"))
                .ReverseMap();

            CreateMap<Agent, AgentSignUpDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => $"{src.CompanyName}"))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => $"{src.VatNumber}"))
                .ReverseMap();

            CreateMap<User, AgentUpdateDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Details!.PhoneNumber}"))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => $"{src.Details!.Country}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Details!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details!.Address}"))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => $"{src.Details!.PostalCode}"))
                .ReverseMap();

            CreateMap<Agent, AgentUpdateDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => $"{src.CompanyName}"))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => $"{src.VatNumber}"))
                .ReverseMap();

//---------------------------------------------------------------------------------------------------------------------------

            CreateMap<User, CitizenReadOnlyDTO>()
               .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => $"{src.Citizen!.BirthDate}"))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => $"{src.Citizen!.Gender}"))
               .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => $"{src.Citizen!.Occupation}"))
               .ReverseMap();

            CreateMap<User, CitizenSignUpDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Details!.PhoneNumber}"))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => $"{src.Details!.Country}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Details!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details!.Address}"))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => $"{src.Details!.PostalCode}"))
                .ReverseMap();

            CreateMap<Citizen, CitizenSignUpDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => $"{src.BirthDate}"))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => $"{src.Gender}"))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => $"{src.Occupation}"))
                .ReverseMap();

            CreateMap<User, CitizenUpdateDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Details!.PhoneNumber}"))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => $"{src.Details!.Country}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Details!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details!.Address}"))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => $"{src.Details!.PostalCode}"))
                .ReverseMap();

            CreateMap<Citizen, CitizenUpdateDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => $"{src.BirthDate}"))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => $"{src.Gender}"))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => $"{src.Occupation}"))
                .ReverseMap();

            //---------------------------------------------------------------------------------------------------------------------------

            //toDo : Recomm Mapping

            //---------------------------------------------------------------------------------------------------------------------------

            //toDo : Dest Mapping
        }
    }
}
