namespace QADataServices.CachingLayer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using ForumServiceContract;
    using QAModels.Forum;
    using QAModels.Membership;

    public class CachingForumService: IForumService
    {
        private readonly IForumService _forumService;

        public CachingForumService(IForumService forumService)
        {
            _forumService = forumService;
        }

        public Forum GetForumById(int forumId)
        {
            return _forumService.GetForumById(forumId);
        }

        public IEnumerable<Forum> GetAllForums()
        {
            return _forumService.GetAllForums();
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

        public IEnumerable<Thread> GetAllThreads()
        {
            return _forumService.GetAllThreads();
        }

        public IEnumerable<Thread> GetThreadsByForum(int forumId)
        {
            return _forumService.GetThreadsByForum(forumId);
        }

        public Thread GetThreadById(int threadId)
        {
            return _forumService.GetThreadById(threadId);
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

        public IEnumerable<Post> FindPosts(string query)
        {
            return _forumService.FindPosts(query);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return FetchOrCache(() => _forumService.GetAllPosts(), "AllPosts");
        }

        public IEnumerable<Post> GetPostsByThread(int threadId)
        {
            return _forumService.GetPostsByThread(threadId);
        }

        public Post GetPostById(int postId)
        {
            return _forumService.GetPostById(postId);
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

        public User GetUserById(Guid userId)
        {
            return _forumService.GetUserById(userId);
        }

        public Member GetMemberById(Guid userId)
        {
            return _forumService.GetMemberById(userId);
        }

        public void UpdateMember(Member member)
        {
            _forumService.UpdateMember(member);
        }

        public bool IsMemberAuthorized(string username)
        {
            return _forumService.IsMemberAuthorized(username);
        }

        private TResult FetchOrCache<TResult>(Func<TResult> generator, string cacheKey)
        {
            var cache = MemoryCache.Default;
            if (cache[cacheKey] == null)
                cache.Set(cacheKey, generator(), DateTimeOffset.Now.AddSeconds(30));
            return (TResult)cache[cacheKey];
        }
    }
}