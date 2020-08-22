namespace TjommeMetSomme.Entities
{
    public class TutorCourse
    {
        public int TutorId { get; set; }

        public Tutor Tutor { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}