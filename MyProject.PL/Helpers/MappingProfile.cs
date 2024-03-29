using AutoMapper;
using MyProject.DAL.Models;
using MyProject.PL.ViewModels;

namespace MyProject.PL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap(); 
               
        }
    }
}
