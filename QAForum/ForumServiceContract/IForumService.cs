namespace ForumServiceContract
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using QAModels;
    using QAModels.Forum;

    [ServiceContract]
    public interface IForumService
    {
        [OperationContract]
        Forum GetForumById(int forumId);

        [OperationContract]
        IEnumerable<Forum> GetAllForums();

        [OperationContract]
        void AddForum(Forum forum);

        [OperationContract]
        void UpdateForum(Forum forum);

        [OperationContract]
        void DeleteForum(Forum forum);

        [OperationContract]
        IEnumerable<Thread> GetAllThreads();

        [OperationContract]
        IEnumerable<Thread> GetThreadsByForum(int forumId);

        [OperationContract]
        Thread GetThreadById(int threadId);

        [OperationContract]
        void AddThread(Thread thread);

        [OperationContract]
        void UpdateThread(Thread thread);

        [OperationContract]
        void DeleteThread(Thread thread);

        [OperationContract]
        IEnumerable<Post> FindPosts(string query);

        [OperationContract]
        IEnumerable<Post> GetAllPosts();

        [OperationContract]
        IEnumerable<Post> GetPostsByThread(int threadId);

        [OperationContract]
        Post GetPostById(int postId);

        [OperationContract]
        void AddPost(Post post);

        [OperationContract]
        void UpdatePost(Post post);

        [OperationContract]
        void DeletePost(Post post);

        [OperationContract]
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        User GetUserById(Guid userId);
    }
}
