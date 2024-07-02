using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.Api.Controllers
{
    [Authorize]  // Requiere autenticación para todos los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]  // Autorización basada en roles
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] EmployeeRequestDTO employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee DTO is null.");
            }

            await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(Get), new { id = 1 }, employee);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeRequestDTO employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee DTO is null.");
            }

            await _employeeService.UpdateEmployeeAsync(employee, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
