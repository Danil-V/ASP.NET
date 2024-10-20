using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Sevices.Interfaces;
using PromoCodeFactory.Sevices.Sevices;
using PromoCodeFactory.WebHost.Models;


namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IRepository<Employee> employeeRepository)   {
            _employeeRepository = employeeRepository;
            _employeeService = new EmployeeService(employeeRepository);
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Добавить нового сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmployeeAsync(ShortEmployee newEmployeeData) {
            if (newEmployeeData == null)
                return BadRequest("Invalid employee data");

            var employeeId = Guid.NewGuid();
            var newEmployee = await _employeeService.EmployeeGetDtoAsync(employeeId, newEmployeeData);
            var employeeCreated = _employeeRepository.CreateAsync(newEmployee);

            if (employeeCreated != null)
                return Ok("Employee has been successfully edded");
            else
                return StatusCode(500, "An error occurred while adding the employee.");
        }

        /// <summary>
        /// Обновить данные сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid id, ShortEmployee updateEmployeeData)
        {
            if (updateEmployeeData == null)
                return BadRequest("Invalid employee data.");
            
            var employee = await _employeeService.EmployeeGetDtoAsync(id, updateEmployeeData);
            var result = await _employeeRepository.UpdateAsync(employee);

            if (result != null)
                return Ok(result);
            else
                return NotFound($"Employee with ID: {id} not found");
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
        {
            var result = await _employeeRepository.DeleteAsync(id);

            if (result)
                return Ok($"Employee with ID: {id} was successful deleted.");
            else
                return NotFound($"Employee with ID: {id} not found");
        }
    }
}