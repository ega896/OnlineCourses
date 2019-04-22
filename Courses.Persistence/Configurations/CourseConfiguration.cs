using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(260);

            builder.HasOne(p => p.File)
                .WithOne(i => i.Course)
                .HasForeignKey<AppFile>(b => b.CourseId);
        }
    }
}
