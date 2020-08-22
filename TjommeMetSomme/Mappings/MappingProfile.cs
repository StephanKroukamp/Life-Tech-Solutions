using AutoMapper;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Resources;


namespace TjommeMetSomme.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to Resource
            CreateMap<Parent, ParentResource>();

            CreateMap<Student, StudentResource>()
                .ForMember(student => student.Parent, opt => opt.MapFrom(resource => resource.Parent));

            // Resource to Entity
            CreateMap<ParentResource, Parent>();
            CreateMap<SaveParentResource, Parent>();

            CreateMap<StudentResource, Student>();
            CreateMap<SaveStudentResource, Student>();

            CreateMap<SignUpResource, User>()
                .ForMember(user => user.UserName, opt => opt.MapFrom(resource => resource.UserName))
                .ForMember(user => user.Email, opt => opt.MapFrom(resource => resource.Email))
                .ForMember(user => user.FirstName, opt => opt.MapFrom(resource => resource.FirstName))
                .ForMember(user => user.LastName, opt => opt.MapFrom(resource => resource.LastName));
        }
    }
}