namespace QADataServices.DataLayer
{
    using System;
    using System.Collections.Generic;
    using QAModels.Forum;
    using QAModels.Membership;

    public interface ForumRepository
    {
        IEnumerable<Forum> GetAllForums();
        Forum GetForumById(int forumId);
        IEnumerable<Thread> GetAllThreads();
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Thread> GetThreadsByForum(int forumId);
        Thread GetThreadById(int threadId);
        IEnumerable<Post> GetPostsByThread(int threadId);
        Post GetPostById(int postId);
        IEnumerable<Post> FindPosts(string query);
        User GetUserById(Guid userId);
        void AddForum(Forum forum);
        void UpdateForum(Forum forum);
        void DeleteForum(Forum forum);
        void AddThread(Thread thread);
        void UpdateThread(Thread thread);
        void DeleteThread(Thread thread);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        IEnumerable<User> GetAllUsers();
        Member GetMemberById(Guid userId);
        void UpdateMember(Member member);
        bool IsMemberAuthorized(string username);
    }
}