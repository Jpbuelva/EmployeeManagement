using EmployeeManagement.Domain.Emun;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int Document { get; set; }

        public string Name { get; set; }

        public PositionType CurrentPosition { get; set; }

        private decimal _baseSalary;
        private decimal _bonus;

        public decimal Salary
        {
            get
            {
                return _baseSalary + _bonus;
            }
            set
            {
                _baseSalary = value;
                CalculateAnnualBonus();
            }
        }

        public List<PositionHistory> PositionHistories { get; set; } = new List<PositionHistory>();

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

        public decimal CalculateAnnualBonus()
        {
            var lastPositionStartDate = PositionHistories.Count > 0 ? PositionHistories[^1].StartDate : DateTime.MinValue;

            if (lastPositionStartDate == DateTime.MinValue || (DateTime.Now - lastPositionStartDate).TotalDays < 365)
            {
                _bonus = 0;
            }
            else
            {
                switch (CurrentPosition)
                {
                    case PositionType.Manager:
                    case PositionType.SeniorManager:
                        _bonus = _baseSalary * 0.2m;
                        break;
                    default:
                        _bonus = _baseSalary * 0.1m;
                        break;
                }
            }

            return _bonus;
        }
    }

    public class PositionHistory
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string Position { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        [JsonIgnore]
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }

    public class EmployeeProject
    {
    
        public int EmployeeId { get; set; }    
        public Employee Employee { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
