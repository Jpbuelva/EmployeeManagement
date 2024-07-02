using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.DTOs
{
    public class EmployeeRequestDTO
    {
        public int Id { get; set; }
        public int Document { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int ProjectId { get; set; } 

    }

    public class RequestPositionHistoryDTO
    {

        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class RequestEmployeeProjectDTO
    {

        public ProjectDTO Project { get; set; }
    }

    public class RequestProjectDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
    }

     
}
