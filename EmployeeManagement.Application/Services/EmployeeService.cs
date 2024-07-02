using AutoMapper;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Emun;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Interfaces;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            foreach (var employee in employees)
            {
                employee.CalculateAnnualBonus();
            }

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeResponseDTO>>(employees);
            return employeeDtos;
        }

        public async Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return null;

            employee.CalculateAnnualBonus();

            return _mapper.Map<EmployeeResponseDTO>(employee);
        }

        public async Task<EmployeeRequestDTO> AddEmployeeAsync(EmployeeRequestDTO employeeDto)
        {

            var employee = new Employee
            {
                DepartmentId = employeeDto.DepartmentId,
                Document = employeeDto.Document,
                Name = employeeDto.Name,
                CurrentPosition = (
                PositionType)employeeDto.CurrentPosition,
                Salary = employeeDto.Salary
            };

            var createdEmployee = await _employeeRepository.AddAsync(employee);
            if (createdEmployee != null)
            {
                var newPositionHistory = new PositionHistory
                {
                    Position = GetPositionById(employeeDto.CurrentPosition),
                    StartDate = DateTime.UtcNow,
                    EmployeeId = employee.Id
                };
                await _employeeRepository.AddPositionHistoryAsync(newPositionHistory);
                await _employeeRepository.AddEmployeeProjectAsync(createdEmployee.Id, employeeDto.ProjectId);
            }
            return _mapper.Map<EmployeeRequestDTO>(createdEmployee);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeRequestDTO employeeDto, int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return false;

            existingEmployee.Salary = employeeDto.Salary;
            existingEmployee.CurrentPosition= (PositionType)employeeDto.CurrentPosition;
             

            var newPositionHistory = new PositionHistory
            {
                Position = GetPositionById(employeeDto.CurrentPosition),
                StartDate = DateTime.UtcNow,
                EmployeeId = id
            };

            existingEmployee.PositionHistories.Add(newPositionHistory);

            return await _employeeRepository.UpdateAsync(existingEmployee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        private string GetPositionById(int id)
        {

            switch (id)
            {               
                case 1:
                    return PositionType.Manager.ToString();
                case 2:
                    return PositionType.SeniorManager.ToString();
                default:
                    return PositionType.RegularEmployee.ToString(); 
            }
        }

    }
}
