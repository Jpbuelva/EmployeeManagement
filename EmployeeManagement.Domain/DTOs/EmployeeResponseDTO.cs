namespace EmployeeManagement.Domain.DTOs
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public int Document { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; }
        public decimal Salary { get; set; }

        public List<PositionHistoryDTO> PositionHistories { get; set; }
   
        public List<EmployeeProjectDTO> EmployeeProjects { get; set; }
        public DepartmentDTO Department { get; set; }
    }

    public class PositionHistoryDTO
    {
   
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EmployeeProjectDTO
    {
  
        public ProjectDTO Project { get; set; }
    }

    public class ProjectDTO
    {
     
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentDTO
    {
   
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
