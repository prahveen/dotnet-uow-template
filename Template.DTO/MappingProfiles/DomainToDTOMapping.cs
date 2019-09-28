using AutoMapper;
using Template.DTO.DTOLibrary;
using Template.Entities.Entities;

namespace Template.DTO.MappingProfiles
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<DefaultEntitiy, DefaultDTO>();
        }
       
    }
}
