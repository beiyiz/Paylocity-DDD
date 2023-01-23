﻿using Microsoft.EntityFrameworkCore;
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
                        CreateEmployee());

                    await _context.SaveChangesWithIdentityInsert<Employee>();
                }

                if (!await _context.DependentTypes.AnyAsync())
                {
                    var depTypes = CreateDependentTypes();
                    await _context.DependentTypes.AddRangeAsync(depTypes);
                    await _context.SaveChangesWithIdentityInsert<DependentType>();
                }

               

 
                if (!await _context.Dependents.AnyAsync())
                {
                    var employee = _context.Employees.FirstOrDefault(c => c.FirstName == "Beiyi" && c.LastName=="Zheng");
                    var dependentType = _context.DependentTypes.FirstOrDefault(c => c.TypeName == "Spouse");
                    await _context.Dependents.AddRangeAsync(CreateDependents(employee.Id, dependentType.Id));

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

       

        private Employee CreateEmployee()
        {
            return new Employee("Beiyi", "Zheng", null);
        }

        private List<DependentType> CreateDependentTypes()
        {
           
            var depTypes = new List<DependentType>()
            {
                new DependentType("Spouse", "Husband or Wife"),
                new DependentType("Child", "Child")
            };

            return depTypes;        
        }

        
        private static List<Dependent> CreateDependents(int empId, int dependentTypeId)
        {
            return new List<Dependent>()
            {
                new Dependent(empId, "Beiyi", "Wilson", dependentTypeId)
            };
        }

    }
}