namespace QAForum.Mapping
{
    using System.Collections.Generic;
    using AutoMapper;
    using Models.ViewModels;
    using QAModels;
    using QAModels.Forum;

    public static class ViewModelMapping
    {
         public static void MapModelsToViewModels()
         {
             Mapper.CreateMap<Post, PostViewModel>();
             Mapper.CreateMap<Forum, ForumViewModel>();
             Mapper.CreateMap<Thread, ThreadViewModel>();
             Mapper.CreateMap<User, UserViewModel>();
             Mapper.CreateMap<KeyValuePair<string, int>, PostTagViewModel>()
                   .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Key))
                   .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Value));
         }

        public static void MapViewModelsToModels()
        {
            Mapper.CreateMap<PostViewModel, Post>();
            Mapper.CreateMap<ForumViewModel, Forum>();
            Mapper.CreateMap<ThreadViewModel, Thread>();
            Mapper.CreateMap<UserViewModel, User>();
        }
    }
}