using System.Collections.Generic;

namespace TjommeMetSomme.Resources
{
    public class CourseResource
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public ICollection<int> StudentIds { get; set; }
    }
}