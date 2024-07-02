using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Infrastructure.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<PositionHistory> AddPositionHistoryAsync(PositionHistory history);
        Task<bool> AddEmployeeProjectAsync(int employeeId, int projectId);
    }
}
