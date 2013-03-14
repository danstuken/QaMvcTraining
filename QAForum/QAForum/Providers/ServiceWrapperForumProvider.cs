namespace QAForum.Providers
{
    using System;
    using System.Collections.Generic;
    using ForumServiceContract;
    using QAModels;
    using QAModels.Forum;

    public class ServiceWrapperForumProvider: ForumProvider
    {
        private readonly IForumService _forumService;

        public ServiceWrapperForumProvider(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IEnumerable<Forum> GetAllForums()
        {
            return _forumService.GetAllForums();
        }

        public Forum GetForumById(int forumId)
        {
            return _forumService.GetForumById(forumId);
        }

        public IEnumerable<Thread> GetAllThreads()
        {
            return _forumService.GetAllThreads();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _forumService.GetAllPosts();
        }

        public IEnumerable<Thread> GetThreadsByForum(int forumId)
        {
            return _forumService.GetThreadsByForum(forumId);
        }

        public Thread GetThreadById(int threadId)
        {
            return _forumService.GetThreadById(threadId);
        }

        public IEnumerable<Post> GetPostsByThread(int threadId)
        {
            return _forumService.GetPostsByThread(threadId);
        }

        public Post GetPostById(int postId)
        {
            return _forumService.GetPostById(postId);
        }

        public IEnumerable<Post> FindPosts(string query)
        {
            return _forumService.FindPosts(query);
        }

        public User GetUserById(Guid userId)
        {
            return _forumService.GetUserById(userId);
        }

        public void AddForum(Forum forum)
        {
            _forumService.AddForum(forum);
        }

        public void UpdateForum(Forum forum)
        {
            _forumService.UpdateForum(forum);
        }

        public void DeleteForum(Forum forum)
        {
            _forumService.DeleteForum(forum);
        }

        public void AddThread(Thread thread)
        {
            _forumService.AddThread(thread);
        }

        public void UpdateThread(Thread thread)
        {
            _forumService.UpdateThread(thread);
        }

        public void DeleteThread(Thread thread)
        {
            _forumService.DeleteThread(thread);
        }

        public void AddPost(Post post)
        {
            _forumService.AddPost(post);
        }

        public void UpdatePost(Post post)
        {
            _forumService.UpdatePost(post);
        }

        public void DeletePost(Post post)
        {
            _forumService.DeletePost(post);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _forumService.GetAllUsers();
        }
    }
}