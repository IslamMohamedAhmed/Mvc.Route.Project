using AutoMapper;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() {

            CreateMap<ApplicationUser, UserViewModel>().ForMember(d => d.Roles, o => o.Ignore());
        }
    }
}
