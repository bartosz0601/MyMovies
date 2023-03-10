using Application.ExternalAPI;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, Movie>();
            CreateMap<MovieExtDTO, Movie>().
                ForMember(m => m.Id, d => d.MapFrom(x => Guid.NewGuid()));
        } 
    }
}
