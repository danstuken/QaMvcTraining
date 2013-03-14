namespace QADataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;
    using ForumServiceContract;
    using QAModels.Forum;
    using QAModels.Membership;

    public class ForumService : IForumService
    {
        private readonly ForumRepository _forumRepository;

        public ForumService(ForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public Forum GetForumById(int forumId)
        {
            return _forumRepository.GetForumById(forumId);
        }

        public IEnumerable<Forum> GetAllForums()
        {
            var result = _forumRepository.GetAllForums();

            return result.ToList();
        }

        public void AddForum(Forum forum)
        {
            _forumRepository.AddForum(forum);
        }

        public void UpdateForum(Forum forum)
        {
            _forumRepository.UpdateForum(forum);
        }

        public void DeleteForum(Forum forum)
        {
            _forumRepository.DeleteForum(forum);
        }

        public IEnumerable<Thread> GetAllThreads()
        {
            return _forumRepository.GetAllThreads();
        }

        public IEnumerable<Thread> GetThreadsByForum(int forumId)
        {
            return _forumRepository.GetThreadsByForum(forumId);
        }

        public Thread GetThreadById(int threadId)
        {
            return _forumRepository.GetThreadById(threadId);
        }

        public void AddThread(Thread thread)
        {
            _forumRepository.AddThread(thread);
        }

        public void UpdateThread(Thread thread)
        {
            _forumRepository.UpdateThread(thread);
        }

        public void DeleteThread(Thread thread)
        {
            _forumRepository.DeleteThread(thread);
        }

        public IEnumerable<Post> FindPosts(string query)
        {
            return _forumRepository.FindPosts(query);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _forumRepository.GetAllPosts();
        }

        public IEnumerable<Post> GetPostsByThread(int threadId)
        {
            return _forumRepository.GetPostsByThread(threadId);
        }

        public Post GetPostById(int postId)
        {
            return _forumRepository.GetPostById(postId);
        }

        public void AddPost(Post post)
        {
            _forumRepository.AddPost(post);
        }

        public void UpdatePost(Post post)
        {
            _forumRepository.UpdatePost(post);
        }

        public void DeletePost(Post post)
        {
            _forumRepository.DeletePost(post);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _forumRepository.GetAllUsers();
        }

        public User GetUserById(Guid userId)
        {
            return _forumRepository.GetUserById(userId);
        }

        public Member GetMemberById(Guid userId)
        {
            return _forumRepository.GetMemberById(userId);
        }

        public void UpdateMember(Member member)
        {
            _forumRepository.UpdateMember(member);
        }

        public bool IsMemberAuthorized(string username)
        {
            return _forumRepository.IsMemberAuthorized(username);
        }
    }
}
