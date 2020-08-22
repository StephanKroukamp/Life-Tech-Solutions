using System.Collections.Generic;

namespace TjommeMetSomme.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<StudentCourse> StudentCourses { get; set; }

        public IList<TutorCourse> TutorCourses { get; set; }
    }
}