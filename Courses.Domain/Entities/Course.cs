using System;

namespace Courses.Domain.Entities
{
    public class Course : BaseEntity
    {      
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual AppFile File { get; set; }

        public virtual User User { get; set; }

        public Guid UserId { get; set; }
    }
}
