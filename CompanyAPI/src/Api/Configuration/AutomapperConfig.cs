using Api.ViewModels;
using AutoMapper;
using Model;

namespace Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Company, CompanyViewModel>().ReverseMap();
        }
    }
}
