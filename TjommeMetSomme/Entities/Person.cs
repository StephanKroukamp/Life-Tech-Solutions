using TjommeMetSomme.Entities.Identity;

namespace TjommeMetSomme.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public int ApplicationUserId { get; set; }

        public int ApplicationRoleId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}