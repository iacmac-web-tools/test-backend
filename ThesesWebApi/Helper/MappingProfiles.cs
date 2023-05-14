using AutoMapper;
using ThesesWebApi.Dto;
using ThesesWebApi.Models;

namespace ThesesWebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ThesisDto, Thesis>().ReverseMap();
            CreateMap<Thesis, ThesisTable>().ReverseMap();
        }
    }
}
