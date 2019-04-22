using System;
using Courses.Domain.Entities;
using Courses.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Courses.Tests.Infrastructure
{
    public class ApplicationDbContextTestFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();
            
            CoursesInitializer.Initialize(context);

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}