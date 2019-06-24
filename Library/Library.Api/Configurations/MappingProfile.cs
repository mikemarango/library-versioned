using AutoMapper;
using Library.Api.Helpers;
using Library.Api.Models;
using Library.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(aDto => aDto.Name, 
                    opt => opt.MapFrom(a => $"{a.FirstName} {a.LastName}"))
                .ForMember(aDto => aDto.Age, 
                    opt => opt.MapFrom(a => a.DateOfBirth.GetCurrentAge()));

            CreateMap<AuthorCreateDto, Author>();
            CreateMap<Book, BookDto>();
            CreateMap<BookCreateDto, Book>();
        }
    }
}
