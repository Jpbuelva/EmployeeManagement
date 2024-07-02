using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Interfaces;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _context.Employees
          .Include(e => e.Department)
          .Include(e => e.PositionHistories)
          .Include(e => e.EmployeeProjects)
              .ThenInclude(ep => ep.Project)
          .ToListAsync();

            return employees;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.Include(e => e.PositionHistories).Include(e => e.Department) 
          .Include(e => e.EmployeeProjects)
              .ThenInclude(ep => ep.Project).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<PositionHistory> AddPositionHistoryAsync(PositionHistory history)
        {
            _context.PositionHistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }
        public async Task<bool> AddEmployeeProjectAsync(int employeeId, int projectId)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(r => r.Id == projectId);
            if (project == null) return false;

            var employeeProject = new EmployeeProject { EmployeeId = employeeId, ProjectId = project.Id };
            _context.EmployeeProjects.Add(employeeProject);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
