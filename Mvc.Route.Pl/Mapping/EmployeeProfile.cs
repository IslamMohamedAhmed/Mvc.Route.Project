using AutoMapper;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
