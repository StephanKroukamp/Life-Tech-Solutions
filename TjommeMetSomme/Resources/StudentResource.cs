namespace TjommeMetSomme.Resources
{
    public class StudentResource : PersonResource
    {
        public ParentResource Parent { get; set; }
    }
}