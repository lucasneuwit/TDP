using AutoMapper;
using TDP.Models.Domain;

namespace TDP.Models.Application.DataTransfer
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            // TODO: use single CreateMap.
            CreateMap<Movie, MovieDTO>().ForMember(dto => dto.Id, src => src.MapFrom(src => src.Id)).ForMember(dto => dto.Title, src => src.MapFrom(src => src.Title)).
                ForMember(dto => dto.Plot, src => src.MapFrom(src => src.Plot)).ForMember(dto => dto.Runtime, src => src.MapFrom(src => src.Runtime)).
                ForMember(dto => dto.Type, src => src.MapFrom(src => src.Type)).ForMember(dto => dto.Released, src => src.MapFrom(src => src.Released)).
                ForMember(dto => dto.Country, src => src.MapFrom(src => src.Country)).ForMember(dto => dto.imdbRating, src => src.MapFrom(src => src.ImdbRating)).
                ForMember(dto => dto.Poster, src => src.MapFrom(src => src.PosterUrl)).ForMember(dto => dto.Actors, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)0).Select(part => part.Name)))).
                ForMember(dto => dto.Director, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)1).Select(part => part.Name)))).
                ForMember(dto => dto.Writer, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)2).Select(part => part.Name))));
            
            CreateMap<Series,SeriesDTO>().ForMember(dto => dto.Id, src => src.MapFrom(src => src.Id)).ForMember(dto => dto.Title, src => src.MapFrom(src => src.Title)).
                ForMember(dto => dto.Plot, src => src.MapFrom(src => src.Plot)).ForMember(dto => dto.Runtime, src => src.MapFrom(src => src.Runtime)).
                ForMember(dto => dto.Type, src => src.MapFrom(src => src.Type)).ForMember(dto => dto.Released, src => src.MapFrom(src => src.Released)).
                ForMember(dto => dto.Country, src => src.MapFrom(src => src.Country)).ForMember(dto => dto.imdbRating, src => src.MapFrom(src => src.ImdbRating)).
                ForMember(dto => dto.Poster, src => src.MapFrom(src => src.PosterUrl)).ForMember(dto => dto.Actors, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)0).Select(part => part.Name)))).
                ForMember(dto => dto.Director, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)1).Select(part => part.Name)))).
                ForMember(dto => dto.Writer, src => src.MapFrom(src => string.Join(", ", src.Participants.Where(p => p.Role == (ParticipantRole)2).Select(part => part.Name)))).
                ForMember(dto=> dto.totalSeasons, src => src.MapFrom(src => src.Seasons));




        }
    }
}