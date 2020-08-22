using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TjommeMetSomme.Entities
{
    public class Parent
    {
        public Parent()
        {
            Students = new Collection<Student>();
        }

        public int ParentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}