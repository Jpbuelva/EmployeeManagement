using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Models
{
    public class RegisterModel

    {
        public string Username { get; set; }
  
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
