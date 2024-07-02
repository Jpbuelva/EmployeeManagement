using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeRequestDTO> AddEmployeeAsync(EmployeeRequestDTO employeeDto);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync();
        Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);
        Task<bool> UpdateEmployeeAsync(EmployeeRequestDTO employeeDto, int id);
    }
}
