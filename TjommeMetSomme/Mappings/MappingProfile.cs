using System.Linq;
using AutoMapper;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Entities.Identity;
using TjommeMetSomme.Resources;

namespace TjommeMetSomme.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to Resource
            CreateMap<Parent, ParentResource>()
                .ForMember(parentResource => parentResource.Email, opt => opt.MapFrom(parent => parent.ApplicationUserId));

            CreateMap<Student, StudentResource>()
                .ForMember(studentResource => studentResource.Parent, opt => opt.MapFrom(student => student.Parent));

            CreateMap<Course, CourseResource>()
                .ForMember(courseResource => courseResource.StudentIds, opt => opt.MapFrom(course => course.StudentCourses.Select(studentCourse => studentCourse.StudentId).ToList()));

            // Resource to Entity
            CreateMap<ParentResource, Parent>();
            CreateMap<SaveParentResource, Parent>();

            CreateMap<StudentResource, Student>();
            CreateMap<SaveStudentResource, Student>();

            CreateMap<CourseResource, Course>();
            CreateMap<SaveCourseResource, Course>();

            CreateMap<SavePersonResource, ApplicationUser>();
        }
    }
}