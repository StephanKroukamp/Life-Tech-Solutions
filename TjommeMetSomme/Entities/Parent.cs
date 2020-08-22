using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TjommeMetSomme.Entities
{
    public class Parent : Person
    {
        public ICollection<Student> Students { get; set; }

        public Parent()
        {
            Students = new Collection<Student>();
        }
    }
}