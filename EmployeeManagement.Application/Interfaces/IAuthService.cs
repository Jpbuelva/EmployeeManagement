using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<bool> RegisterAsync(RegisterModel model);
    }
}
