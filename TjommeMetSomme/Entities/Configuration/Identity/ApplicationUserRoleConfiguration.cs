using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TjommeMetSomme.Entities.Identity;

namespace TjommeMetSomme.Entities.Configuration.Identity
{
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            // Administrator
            builder.HasData(new ApplicationUserRole
            {
                UserId = Constants.Administator.User.ID,
                RoleId = Constants.Administator.Role.ID
            });

            // Manager
            builder.HasData(new ApplicationUserRole
            {
                UserId = Constants.Manager.User.ID,
                RoleId = Constants.Manager.Role.ID
            });
        }
    }
}