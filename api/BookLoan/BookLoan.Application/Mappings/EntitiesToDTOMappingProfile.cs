
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Domain.Entities;

namespace BookLoan.Application.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {

        public EntitiesToDTOMappingProfile()
        {

            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<LoanDTO, Domain.Entities.Loan>().ReverseMap()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(x => x.Book))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(x => x.Client));
            CreateMap<Domain.Entities.Loan, LoanInsertDTO>().ReverseMap();
        }

    }
}
