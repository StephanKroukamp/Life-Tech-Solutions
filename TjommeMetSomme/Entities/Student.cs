using System.Collections.Generic;

namespace TjommeMetSomme.Entities
{
    public class Student : Person
    {
        public int ParentId { get; set; }

        public Parent Parent { get; set; }

        public IList<StudentCourse> StudentCourses { get; set; }
    }
}