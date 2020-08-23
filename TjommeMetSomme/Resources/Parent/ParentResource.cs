using System.Collections.Generic;

namespace TjommeMetSomme.Resources
{
    public class ParentResource : PersonResource
    {
        public ICollection<StudentResource> Students { get; set; }
    }
}