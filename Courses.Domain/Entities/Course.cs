namespace Courses.Domain.Entities
{
    public class Course : BaseEntity
    {      
        public string Name { get; set; }

        public string Description { get; set; }

        public string AvatarFileName { get; set; }
    }
}
