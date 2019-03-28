using System;
using System.Linq;
using Courses.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

            if (!EnumerableExtensions.Any(context.Users)) SeedUsers(context);

            if (!EnumerableExtensions.Any(context.Courses)) SeedCourses(context);
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            var hasher = new PasswordHasher<User>();

            var users = new[]
            {
                new User
                {
                    UserName = "mainadmin",
                    NormalizedUserName = "MAINADMIN",
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    Email = "admin@courses.com",
                    PasswordHash = hasher.HashPassword(null, "admin123"),
                    SecurityStamp = string.Empty
                },
                new User
                {
                    UserName = "ega896",
                    NormalizedUserName = "EGA896",
                    FirstName = "Egor",
                    LastName = "Kenda",
                    EmailConfirmed = true,
                    Email = "egor.kenney@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "ega896"),
                    SecurityStamp = string.Empty
                }
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }

        private static void SeedCourses(ApplicationDbContext context)
        {
            var courses = new[]
            {
                new Course { Name = "Happy English", Description = "For english-beginner students", UserId = context.Users.First().Id },
                new Course { Name = "Blue drawing with me", Description = "Child drawing courses", UserId = context.Users.First().Id},
                new Course { Name = "Computer science", Description = "Practical courses for students", UserId = context.Users.Last().Id}
            };

            context.Courses.AddRange(courses);

            context.SaveChanges();
        }
    }
}
