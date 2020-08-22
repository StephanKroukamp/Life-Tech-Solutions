using System.Collections.Generic;

namespace TjommeMetSomme.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public IList<StudentCourse> StudentCourses { get; set; }

        public IList<TutorCourse> TutorCourses { get; set; }
    }
}