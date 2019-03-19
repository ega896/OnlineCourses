using System;

namespace Courses.Domain.Entities
{
    public class AppFile : BaseEntity
    {
        public string Caption { get; set; }

        public string Name { get; set; }

        public virtual Course Course { get; set; }

        public Guid CourseId { get; set; }
    }
}