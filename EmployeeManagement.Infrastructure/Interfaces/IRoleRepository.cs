using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<bool> CreateRoleAsync(Role role);
        Task<bool> AssignRoleToUserAsync(int userId, string roleName);
        Task<IEnumerable<string>> GetRolesForUserAsync(int userId);
    }
}
