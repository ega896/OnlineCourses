using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Courses.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
