using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TjommeMetSomme.Entities.Identity;

namespace TjommeMetSomme.Entities.Configuration.Identity
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            // Administrator
            builder.HasData(new ApplicationRole
            {
                 Id = Constants.Administator.Role.ID,
                 Name = Constants.Administator.Role.NAME,
                 NormalizedName = Constants.Administator.Role.NORMALIZED_NAME
            });

            // Manager
            builder.HasData(new ApplicationRole
            {
                Id = Constants.Manager.Role.ID,
                Name = Constants.Manager.Role.NAME,
                NormalizedName = Constants.Manager.Role.NORMALIZED_NAME
            });

            // Parent
            builder.HasData(new ApplicationRole
            {
                Id = Constants.Parent.Role.ID,
                Name = Constants.Parent.Role.NAME,
                NormalizedName = Constants.Parent.Role.NORMALIZED_NAME
            });

            // Student
            builder.HasData(new ApplicationRole
            {
                Id = Constants.Student.Role.ID,
                Name = Constants.Student.Role.NAME,
                NormalizedName = Constants.Student.Role.NORMALIZED_NAME
            });

            // Tutor
            builder.HasData(new ApplicationRole
            {
                Id = Constants.Tutor.Role.ID,
                Name = Constants.Tutor.Role.NAME,
                NormalizedName = Constants.Tutor.Role.NORMALIZED_NAME
            });
        }
    }
}