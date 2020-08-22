﻿using AutoMapper;
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

            //CreateMap<Book, BookResource>()
            //    .ForMember(book => book.Author, opt => opt.MapFrom(resource => resource.Author));

            // Resource to Entity
            CreateMap<ParentResource, Parent>();
            CreateMap<SaveParentResource, Parent>();

            //CreateMap<BookResource, Book>();
            //CreateMap<SaveBookResource, Book>();

            CreateMap<SignUpResource, User>()
                .ForMember(user => user.UserName, opt => opt.MapFrom(resource => resource.UserName))
                .ForMember(user => user.Email, opt => opt.MapFrom(resource => resource.Email))
                .ForMember(user => user.FirstName, opt => opt.MapFrom(resource => resource.FirstName))
                .ForMember(user => user.LastName, opt => opt.MapFrom(resource => resource.LastName));
        }
    }
}