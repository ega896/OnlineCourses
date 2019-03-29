using System;
using Courses.Persistence;

namespace Courses.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; }

        public QueryTestFixture()
        {
            Context = ApplicationDbContextTestFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextTestFactory.Destroy(Context);
        }
    }
}