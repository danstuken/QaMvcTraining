namespace QAForum.Mapping
{
    using AutoMapper;
    using Models.Forum;
    using Models.ViewModels;

    public static class ViewModelMapping
    {
         public static void MapModelsToViewModels()
         {
             Mapper.CreateMap<Post, PostViewModel>();
             Mapper.CreateMap<Forum, ForumViewModel>();
             Mapper.CreateMap<Thread, ThreadViewModel>();
         }

        public static void MapViewModelsToModels()
        {
            Mapper.CreateMap<PostViewModel, Post>();
            Mapper.CreateMap<ForumViewModel, Forum>();
            Mapper.CreateMap<ThreadViewModel, Thread>();
        }
    }
}