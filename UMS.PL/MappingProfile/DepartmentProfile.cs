using AutoMapper;
using UMS.DAL.Models;
using UMS.PL.ViewModels;

namespace UMS.PL.MappingProfile
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentVM, Department>().ReverseMap();
        }
    }
}
