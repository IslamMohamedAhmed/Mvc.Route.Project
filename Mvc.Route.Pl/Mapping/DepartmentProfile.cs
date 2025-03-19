using AutoMapper;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}
