using System.Collections.Generic;

namespace TjommeMetSomme.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ParentId { get; set; }

        public Parent Parent { get; set; }

        public IList<StudentCourse> StudentCourses { get; set; }
    }
}