using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(a => a.StudentId);

            builder
                .Property(m => m.StudentId)
                .UseMySqlIdentityColumn();

            builder
                .Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Parent)
                .WithMany(a => a.Students)
                .HasForeignKey(m => m.ParentId);

            builder
                .ToTable("Students");
        }
    }
}