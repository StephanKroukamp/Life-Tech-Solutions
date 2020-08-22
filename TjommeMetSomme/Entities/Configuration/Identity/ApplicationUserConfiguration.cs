using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TjommeMetSomme.Entities.Identity;

namespace TjommeMetSomme.Entities.Configuration.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            // Administrator
            var administrator = new ApplicationUser
            {
                Id = Constants.Administator.User.ID,
                Email = Constants.Administator.User.EMAIL,
                UserName = Constants.Administator.User.USERNAME,
                FirstName = Constants.Administator.User.FIRST_NAME,
                LastName = Constants.Administator.User.LAST_NAME
            };

            administrator.PasswordHash = passwordHasher.HashPassword(administrator, Constants.Administator.User.PASSWORD);

            builder.HasData(administrator);

            // Manager
            var manager = new ApplicationUser
            {
                Id = Constants.Manager.User.ID,
                Email = Constants.Manager.User.EMAIL,
                UserName = Constants.Manager.User.USERNAME,
                FirstName = Constants.Manager.User.FIRST_NAME,
                LastName = Constants.Manager.User.LAST_NAME
            };

            manager.PasswordHash = passwordHasher.HashPassword(manager, Constants.Manager.User.PASSWORD);

            builder.HasData(manager);
        }
    }
}