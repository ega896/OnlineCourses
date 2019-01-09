﻿using Microsoft.EntityFrameworkCore.Internal;
using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Persistence
{
    public class CoursesInitializer
    {
        public static void Initialize(CoursesDbContext context)
        {
            var initializer = new CoursesInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(CoursesDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Courses.Any()) return;

            SeedCourses(context);
        }

        private void SeedCourses(CoursesDbContext context)
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
