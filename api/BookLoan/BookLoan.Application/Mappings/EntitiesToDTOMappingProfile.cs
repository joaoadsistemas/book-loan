
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Domain.Entities;
using BookLoan.Domain.SystemModels;

namespace BookLoan.Application.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {

        public EntitiesToDTOMappingProfile()
        {

            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<LoanDTO, Domain.Entities.Loan>().ReverseMap()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(x => x.Client));
            CreateMap<Domain.Entities.Loan, LoanInsertDTO>().ReverseMap();
            CreateMap<AmountItem, AmountItemDTO>().ReverseMap();
            CreateMap<LoanBook, LoanBookDTO>().ReverseMap()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(x => x.Book));
        }

    }
}
