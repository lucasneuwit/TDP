using TDP.Models.Domain;
using AutoMapper;

namespace TDP.Models.Application.DataTransfer.MappingProfiles
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            // TODO: use single CreateMap.
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Id, src => src.MapFrom(src => src.Id));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Title, src => src.MapFrom(src => src.Title));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Plot, src => src.MapFrom(src => src.Plot));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Runtime, src => src.MapFrom(src => src.Runtime));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Type, src => src.MapFrom(src => src.Type));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Released, src => src.MapFrom(src => src.Released));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Poster, src => src.MapFrom(src => src.PosterUrl));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.Country, src => src.MapFrom(src => src.Country));
            CreateMap<TDP.Models.Domain.Movie, Movie>().ForMember(dto => dto.imdbRating, src => src.MapFrom(src => src.ImdbRating));
        }
    }
}