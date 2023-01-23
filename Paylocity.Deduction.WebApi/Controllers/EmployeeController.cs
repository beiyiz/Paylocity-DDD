using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paylocity.Deduction.Core.Aggregates;
using Paylocity.Deduction.Core.Aggregates.Specification;
using Paylocity.Deduction.SharedKernel.Interface;
using Paylocity.Deduction.WebApi.Models;

namespace Paylocity.Deduction.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeController(IRepository<Employee> repository)
        {
            _repository = repository;
        }

       
        [HttpPost(Name = "GetEmployeeById")]
        public async Task<IActionResult> Get([FromBody] GetEmployeeByIdRequest request)
        {
            var response = new GetEmployeeByIdResponse();

            var spec = new EmployeeByIdIncludeDependentsSpecification(request.Id);
            var employee = await _repository.GetBySpecAsync(spec);

            if (employee is null) return NotFound();

            response.Employee = employee;
            response.Deductables = employee.Calculate();
            return Ok(response);
        }
    }
}