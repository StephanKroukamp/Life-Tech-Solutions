using System;
using Microsoft.AspNetCore.Identity;

namespace TjommeMetSomme.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}