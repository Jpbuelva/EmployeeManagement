using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Emun;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeResponseDTO, Employee>()
                .ForMember(dest => dest.CurrentPosition, opt => opt.MapFrom(src => (PositionType)src.CurrentPosition))
                .ForMember(dest => dest.PositionHistories, opt => opt.MapFrom(src => src.PositionHistories));

            CreateMap<Employee, EmployeeResponseDTO>()
                .ForMember(dest => dest.CurrentPosition, opt => opt.MapFrom(src => (int)src.CurrentPosition))
                .ForMember(dest => dest.PositionHistories, opt => opt.MapFrom(src => src.PositionHistories));

            CreateMap<PositionHistoryDTO, PositionHistory>().ReverseMap();
            CreateMap<EmployeeProjectDTO, EmployeeProject>().ReverseMap();
            CreateMap<ProjectDTO, Project>().ReverseMap();
            CreateMap<DepartmentDTO, Department>().ReverseMap(); 
        }
    }
}
