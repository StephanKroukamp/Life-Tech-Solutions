using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(student => student.StudentId);

            builder
                .Property(student => student.StudentId)
                .UseMySqlIdentityColumn();

            builder
                .Property(student => student.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(student => student.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(student => student.Parent)
                .WithMany(parent => parent.Students)
                .HasForeignKey(student => student.ParentId);

            builder
                .ToTable("Students");
        }
    }
}