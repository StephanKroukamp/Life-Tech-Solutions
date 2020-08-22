using System.Collections.Generic;

namespace TjommeMetSomme.Entities
{
    public class Tutor : Person
    {
        public IList<TutorCourse> TutorCourses { get; set; }
    }
}