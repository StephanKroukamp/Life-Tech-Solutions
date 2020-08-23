using System.Collections.Generic;
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
                    .ForMember(parentResource => parentResource.Id, opt => opt.MapFrom(parent => parent.Id))
                    .ForMember(parentResource => parentResource.Email, opt => opt.MapFrom(parent => parent.ApplicationUser.Email))
                    .ForMember(parentResource => parentResource.UserName, opt => opt.MapFrom(parent => parent.ApplicationUser.UserName))
                    .ForMember(parentResource => parentResource.FirstName, opt => opt.MapFrom(parent => parent.ApplicationUser.FirstName))
                    .ForMember(parentResource => parentResource.LastName, opt => opt.MapFrom(parent => parent.ApplicationUser.LastName))
                    .ForMember(parentResource => parentResource.Role, opt => opt.MapFrom(parent => parent.ApplicationRole.NormalizedName))
                    .ForMember(parentResource => parentResource.Students, opt => opt.MapFrom(parent => parent.Students));

            CreateMap<Student, StudentResource>()
                .ForMember(studentResource => studentResource.Id, opt => opt.MapFrom(student => student.Id))
                .ForMember(studentResource => studentResource.Email, opt => opt.MapFrom(student => student.ApplicationUser.Email))
                .ForMember(studentResource => studentResource.UserName, opt => opt.MapFrom(student => student.ApplicationUser.UserName))
                .ForMember(studentResource => studentResource.FirstName, opt => opt.MapFrom(student => student.ApplicationUser.FirstName))
                .ForMember(studentResource => studentResource.LastName, opt => opt.MapFrom(student => student.ApplicationUser.LastName))
                .ForMember(studentResource => studentResource.Role, opt => opt.MapFrom(student => student.ApplicationRole.NormalizedName))
                .ForMember(studentResource => studentResource.Parent, opt => opt.MapFrom(student => student.Parent));


            CreateMap<Course, CourseResource>()
                .ForMember(courseResource => courseResource.StudentIds, opt => opt.MapFrom(course => course.StudentCourses.Select(studentCourse => studentCourse.StudentId).ToList()));

            // Resource to Entity
            CreateMap<ParentResource, Parent>();
            CreateMap<CreateParentResource, Parent>();

            CreateMap<StudentResource, Student>();
            CreateMap<CreateStudentResource, Student>();

            CreateMap<CourseResource, Course>();
            CreateMap<SaveCourseResource, Course>();

            CreateMap<CreateParentResource, ApplicationUser>();
            CreateMap<CreateStudentResource, ApplicationUser>();
        }
    }
}