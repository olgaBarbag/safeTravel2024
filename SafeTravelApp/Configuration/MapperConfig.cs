using AutoMapper;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.DTO.Agent;
using SafeTravelApp.DTO.Citizen;
using SafeTravelApp.DTO.Destination;
using SafeTravelApp.DTO.Recommendation;
using SafeTravelApp.DTO.User;
using System;

namespace SafeTravelApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserSignupDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new UserDetails
                {
                    PhoneNumber = src.PhoneNumber,
                    Country = src.Country,
                    City = src.City,
                    Address = src.Address,
                    PostalCode = src.PostalCode
                }));

            CreateMap<User, UserUpdateDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new UserDetails
                {
                    PhoneNumber = src.PhoneNumber,
                    Country = src.Country,
                    City = src.City,
                    Address = src.Address,
                    PostalCode = src.PostalCode
                }));

            CreateMap<User, UserReadOnlyDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap();

            CreateMap<User, UserLoginDTO>().ReverseMap();

//---------------------------------------------------------------------------------------------------------------------------

            CreateMap<Agent, AgentReadOnlyDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName != null ? src.CompanyName : string.Empty))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => src.VatNumber != null ? src.VatNumber : string.Empty))
                .ReverseMap();

            CreateMap<User, AgentReadOnlyDTO>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username != null ? src.Username : string.Empty))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email : string.Empty))
               .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname != null ? src.Firstname : string.Empty))
               .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname != null ? src.Lastname : string.Empty))
               .ReverseMap();

            CreateMap<User, AgentDetailsReadOnlyDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.Username}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => $"{src.Firstname}"))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => $"{src.Lastname}"))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => $"{src.UserRole}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap();

            CreateMap<Agent, AgentDetailsReadOnlyDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName != null ? src.CompanyName : string.Empty))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => src.VatNumber != null ? src.VatNumber : string.Empty))
                .ReverseMap();

            CreateMap<User, AgentSignUpDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username != null ? src.Username : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email : string.Empty))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password != null ? src.Password : string.Empty))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname != null ? src.Firstname : string.Empty))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname != null ? src.Lastname : string.Empty))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole != null ? src.UserRole : UserRole.Agent))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new UserDetails
                {
                    PhoneNumber = src.PhoneNumber,
                    Country = src.Country,
                    City = src.City,
                    Address = src.Address,
                    PostalCode = src.PostalCode
                }));

            CreateMap<Agent, AgentSignUpDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName != null ? src.CompanyName : string.Empty))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => src.VatNumber != null ? src.VatNumber : string.Empty))
                .ReverseMap();

            CreateMap<User, AgentUpdateDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap();

            CreateMap<Agent, AgentUpdateDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName != null ? src.CompanyName : string.Empty))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => src.VatNumber != null ? src.VatNumber : string.Empty))
                .ReverseMap();

//---------------------------------------------------------------------------------------------------------------------------

            CreateMap<Citizen, CitizenReadOnlyDTO>()
               .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate : new DateOnly(1900, 1, 1)))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != null ? src.Gender : Gender.None))
               .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation != null ? src.Occupation : string.Empty))
               .ReverseMap();

            CreateMap<User, CitizenReadOnlyDTO>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username != null ? src.Username : string.Empty))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email : string.Empty))
               .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname != null ? src.Firstname : string.Empty))
               .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname != null ? src.Lastname : string.Empty))
               .ReverseMap();

            CreateMap<User, CitizenDetailsReadOnlyDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.Username}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => $"{src.Firstname}"))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => $"{src.Lastname}"))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => $"{src.UserRole}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap();

            CreateMap<Citizen, CitizenDetailsReadOnlyDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate : new DateOnly(1900, 1, 1)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != null ? src.Gender : Gender.None))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation != null ? src.Occupation : string.Empty))
                .ReverseMap();

            CreateMap<User, CitizenSignUpDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username != null ? src.Username : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email : string.Empty))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password != null ? src.Password : string.Empty))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname != null ? src.Firstname : string.Empty))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname != null ? src.Lastname : string.Empty))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole != null ? src.UserRole : UserRole.Citizen))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new UserDetails
                {
                    PhoneNumber = src.PhoneNumber,
                    Country = src.Country,
                    City = src.City,
                    Address = src.Address,
                    PostalCode = src.PostalCode
                }));


            CreateMap<Citizen, CitizenSignUpDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate : new DateOnly(1900, 1, 1)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != null ? src.Gender : Gender.None))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation != null ? src.Occupation : string.Empty))
                .ReverseMap();

            CreateMap<User, CitizenUpdateDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username != null ? src.Username : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email : string.Empty))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password != null ? src.Password : string.Empty))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname != null ? src.Firstname : string.Empty))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname != null ? src.Lastname : string.Empty))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details != null ? src.Details.PhoneNumber : string.Empty))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details != null ? src.Details.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details != null ? src.Details.City : string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Details != null ? src.Details.Address : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Details != null ? src.Details.PostalCode : string.Empty))
                .ReverseMap();

            CreateMap<Citizen, CitizenUpdateDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate : new DateOnly(1900, 1, 1)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != null ? src.Gender : Gender.None))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation != null ? src.Occupation : string.Empty))
                .ReverseMap();

            //---------------------------------------------------------------------------------------------------------------------------

            CreateMap<Recommendation, RecommendationReadOnlyDTO>().ReverseMap();

            CreateMap<User, UserRecommendationReadOnlyDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.Title)) : string.Empty))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.Description)) : string.Empty))
                .ForMember(dest => dest.ContributorRole, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.ContributorRole)) : string.Empty))
                .ForMember(dest => dest.ContributorId, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.ContributorId)) : string.Empty))
                .ReverseMap();

            CreateMap<Destination, DestinationRecommendationReadOnlyDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.Title)) : string.Empty))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.Description)) : string.Empty))
                .ForMember(dest => dest.ContributorRole, opt => opt.MapFrom(src => src.Recommendations != null ? string.Join(", ", src.Recommendations
                .Where(p => p != null).Select(p => p.ContributorRole)) : string.Empty))
                .ReverseMap();

            CreateMap<Destination, RecommendationInsertDTO>().ReverseMap();

            CreateMap<Recommendation, RecommendationInsertDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => src.Destination.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title != null ? src.Title : string.Empty))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description != null ? src.Description : string.Empty))
                .ReverseMap();
                     

            CreateMap<Recommendation, RecommendationUpdateDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title != null ? src.Title : string.Empty))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description != null ? src.Description : string.Empty))
                .ReverseMap();

            //---------------------------------------------------------------------------------------------------------------------------

            CreateMap<Destination, DestinationInsertDTO>().ReverseMap();
            CreateMap<Destination, CitizenDestinationsReadOnlyDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country != null ? src.Country : string.Empty))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City != null ? src.City : string.Empty))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region != null ? src.Region : string.Empty))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type != null ? src.Type : DestinationType.City))
                .ForMember(dest => dest.CitizenRole, opt => opt.MapFrom(src => src.CitizenDestinations != null ? string.Join(", ", src.CitizenDestinations
                                .Where(cd => cd != null).Select(cd => cd.CitizenRole)) : string.Empty))
                .ReverseMap();

            CreateMap<Citizen, CitizenDestinationsReadOnlyDTO>()
                .ForMember(dest => dest.CitizenRole, opt => opt.MapFrom(src => src.CitizenDestinations != null ? string.Join(", ", src.CitizenDestinations
                .Where(p => p != null).Select(p => p.CitizenRole)) : string.Empty))
                .ReverseMap();

            CreateMap<Destination, DestinationUpdateDTO>().ReverseMap();
        }
    }
}
