using Paylocity.Deduction.Infrastructure.Data;
using Paylocity.Deduction.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Threading.Tasks;
using Xunit;
using Paylocity.Deduction.Test.Builder;
using Paylocity.Deduction.Core.Aggregates;

namespace Paylocity.Deduction.Test.EmployeeTest
{
    public class EfRepositoryGetById : IClassFixture<SharedDatabaseFixture>
    {
        public SharedDatabaseFixture Fixture { get; }
        public EfRepositoryGetById(SharedDatabaseFixture fixture) => Fixture = fixture;

        [Fact]
        public async Task GetsByIdClientAfterAddingIt()
        {
            using (var transaction = await Fixture.Connection.BeginTransactionAsync())
            {
                string name = Guid.NewGuid().ToString();
                var employee = new EmployeeBuilder().Build();

                var repo1 = new EfRepository<Employee>(Fixture.CreateContext(transaction));
                await repo1.AddAsync(employee);

                var repo2 = new EfRepository<Employee>(Fixture.CreateContext(transaction));
                var employeeFromDb = (await repo2.GetByIdAsync(employee.Id));

                Assert.Equal(employee.Id, employeeFromDb.Id);
                Assert.Equal(employee.FirstName, employeeFromDb.FirstName);
                Assert.Equal(employee.LastName, employeeFromDb.LastName);
            }
        }
    }
}