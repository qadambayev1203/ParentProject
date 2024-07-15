using AutoMapper;
using Entities.DTO;
using Entities.DTO.ConfReadedDTO;
using Entities.DTO.DepartamentDTOS;
using Entities.Model;
using Entities.Model.DepartamentsModel;
using Entities.Model.FileModel;

namespace TSTUWebAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();




            //Departament DTOS
            CreateMap<DepartamentCreatedDTO, Departament>();
            CreateMap<DepartamentUpdatedDTO, Departament>();
            CreateMap<Departament, DepartamentReadedDTO>();
            CreateMap<Departament, DepartamentReadedSiteDTO>();
            CreateMap<Departament, DepartamentChildReadedSiteDTO>();










            CreateMap<Files, FilesConfReadedDTO>();


        }
    }
}
