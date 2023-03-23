using TDP.Models.Domain;
using AutoMapper;
using TDP.Models.Domain.Enums;

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
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Actors, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)0).Select(part => part.Name))));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Director, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)1).Select(part => part.Name))));
            CreateMap<Domain.Movie, Movie>().ForMember(dto => dto.Writer, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)2).Select(part => part.Name))));




        }
    }
}