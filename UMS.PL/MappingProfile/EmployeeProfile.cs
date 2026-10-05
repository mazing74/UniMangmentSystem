using AutoMapper;
using UMS.DAL.Models;
using UMS.PL.ViewModels;

namespace UMS.PL.MappingProfile
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
}
