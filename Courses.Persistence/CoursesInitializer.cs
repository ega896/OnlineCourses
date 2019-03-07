using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Courses.Persistence
{
    public class CoursesInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            var initializer = new CoursesInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (context.Courses.Any()) return;

            SeedCourses(context);
        }

        private void SeedCourses(ApplicationDbContext context)
        {
            var courses = new[]
            {
                new Course { Name = "Happy English", Description = "For english-beginner students"},
                new Course { Name = "Blue drawing with me", Description = "Child drawing courses"},
                new Course { Name = "Computer science", Description = "Practical courses for students"}
            };

            context.Courses.AddRange(courses);

            context.SaveChanges();
        }
    }
}
