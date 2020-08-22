namespace TjommeMetSomme.Resources
{
    public class StudentResource
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ParentResource Parent { get; set; }
    }
}