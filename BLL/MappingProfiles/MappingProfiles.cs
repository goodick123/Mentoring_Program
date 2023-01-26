using AutoMapper;
using BLL.LibraryFileSystem.DTOs;
using DAL.Models;

namespace BLL.LibraryFileSystem.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Patent, PatentDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<Magazine, MagazineDTO>().ReverseMap();
            CreateMap<LocalizedBook, LocalizedBookDTO>()
                .ForMember(dest => dest.OriginalPublisher, 
                    opt => opt.MapFrom(
                        src => src.Publisher)).ReverseMap();
        }
    }
}
