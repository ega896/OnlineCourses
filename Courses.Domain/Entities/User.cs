using System;
using Microsoft.AspNetCore.Identity;

namespace Courses.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
