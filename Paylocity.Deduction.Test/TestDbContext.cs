using MediatR;
using Microsoft.EntityFrameworkCore;
using Paylocity.Deduction.Infrastructure.Data;




namespace Paylocity.Deduction.Test
{
       internal sealed class TestDbContext : AppDbContext
    {
        public TestDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options, mediator)
        {
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        public override void Dispose()
        {
            Database.CloseConnection();
            base.Dispose();
        }
    }
}