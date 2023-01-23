using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Paylocity.Deduction.Core.Aggregates;
using Paylocity.Deduction.Core.ValueObjects;
using Paylocity.Deduction.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Paylocity.Deduction.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppDbContextSeed> _logger;

        private DateTimeOffset _testDate = DateTimeOffset.UtcNow;
        public AppDbContextSeed(AppDbContext context,
          ILogger<AppDbContextSeed> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync(DateTimeOffset testDate, int? retry = 0)
        {
            _logger.LogInformation($"Seeding data - testDate: {testDate}");
            _logger.LogInformation($"DbContext Type: {_context.Database.ProviderName}");

            _testDate = testDate;
            int retryForAvailability = retry.Value;
            try
            {
               

                if (!await _context.Employees.AnyAsync())
                {
                    await _context.Employees.AddAsync(
                        CreateEmployee("Beiyi","Zheng"));

                    await _context.SaveChangesWithIdentityInsert<Employee>();
                }

                
 
                if (!await _context.Dependents.AnyAsync())
                {
                    var employee = _context.Employees.FirstOrDefault(c => c.FirstName == "Beiyi" && c.LastName=="Zheng");
                    //var dependentType = _context.DependentTypes.FirstOrDefault(c => c.Name == "Spouse");
                    await _context.Dependents.AddRangeAsync(CreateDependents(employee.Id,"Alex","Wilson", new DependentType("Spouse", "Husband")));

                    await _context.SaveChangesWithIdentityInsert<Dependent>();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 1)
                {
                    retryForAvailability++;
                    _logger.LogError(ex.Message);
                    await SeedAsync(_testDate, retryForAvailability);
                }
                throw;
            }

            await _context.SaveChangesAsync();
        }

       

        private Employee CreateEmployee(string firstName, string lastName)
        {
            return new Employee(firstName, lastName);
        }

        
        
        private static List<Dependent> CreateDependents(int empId, string firstName, string lastName, DependentType dependentType)
        {
            var dependent = new Dependent(empId, firstName, lastName);
            dependent.SetDependentType(dependentType);
            return new List<Dependent>()
            {
                dependent
            };
        }

    }
}