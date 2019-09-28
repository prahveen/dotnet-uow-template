using AutoMapper;
using Template.DTO.DTOLibrary;
using Template.Entities.Entities;

namespace Template.DTO.MappingProfiles
{
    public class DTOToDomainMapping : Profile
    {
        public DTOToDomainMapping()
        {
            CreateMap<DefaultDTO,DefaultEntitiy>();
        }
    }
}
