using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(student => student.Id);

            builder
                .Property(student => student.Id)
                .UseMySqlIdentityColumn();

            builder
                .HasOne(student => student.Parent)
                .WithMany(parent => parent.Students)
                .HasForeignKey(student => student.ParentId);

            builder
                .ToTable("Students");
        }
    }
}