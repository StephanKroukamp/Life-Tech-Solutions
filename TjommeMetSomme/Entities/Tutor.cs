using System.Collections.Generic;

namespace TjommeMetSomme.Entities
{
    public class Tutor
    {
        public int TutorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<TutorCourse> TutorCourses { get; set; }
    }
}