using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Paylocity.Deduction.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Paylocity.Deduction.Test
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public SharedDatabaseFixture()
        {
            var connString = @"Data Source=dan\sqlexpress;Initial Catalog=Paylocity.Deduction.IntegrationTest;Integrated Security=True";
            Connection = new SqlConnection(connString);

            Seed();

            Connection.Open();
        }

        public DbConnection Connection { get; }

        public AppDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(Connection).Options, new Mock<IMediator>().Object);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        //context.Database.EnsureCreated();

                        var logger = new LoggerFactory().CreateLogger<AppDbContextSeed>();
                        var appDbContextSeed = new AppDbContextSeed(context, logger);
                        appDbContextSeed.SeedAsync(DateTimeOffset.UtcNow).Wait();
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}