using TDP.Models.Domain;
using AutoMapper;

namespace TDP.Models.Application.DataTransfer.MappingProfiles
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            // TODO: use single CreateMap.
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Id, src => src.MapFrom(src => src.Id));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Title, src => src.MapFrom(src => src.Title));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Plot, src => src.MapFrom(src => src.Plot));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Runtime, src => src.MapFrom(src => src.Runtime));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Type, src => src.MapFrom(src => src.Type));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Released, src => src.MapFrom(src => src.Released));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Country, src => src.MapFrom(src => src.Country));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.imdbRating, src => src.MapFrom(src => src.ImdbRating));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Poster, src => src.MapFrom(src => src.PosterUrl));
        }
    }
}