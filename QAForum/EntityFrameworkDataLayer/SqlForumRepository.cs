namespace EntityFrameworkDataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using QADataServices.DataLayer;
    using QAModels;
    using QAModels.Forum;

    public class SqlForumRepository: ForumRepository
    {
        private readonly ForumContext _forumContext;

        public SqlForumRepository(ForumContext forumContext)
        {
            _forumContext = forumContext;
        }

        IEnumerable<Forum> ForumRepository.GetAllForums()
        {
            return _forumContext.Forums.ToList();
        }

        Forum ForumRepository.GetForumById(int forumId)
        {
            return _forumContext.Forums.Single(f => f.ForumId == forumId);
        }

        IEnumerable<Thread> ForumRepository.GetAllThreads()
        {
            return _forumContext.Threads.ToList();
        }

        IEnumerable<Post> ForumRepository.GetAllPosts()
        {
            return _forumContext.Posts.ToList();
        }

        IEnumerable<Thread> ForumRepository.GetThreadsByForum(int forumId)
        {
            return (from thread in _forumContext.Threads where thread.ForumId == forumId select thread).ToList();
        }

        Thread ForumRepository.GetThreadById(int threadId)
        {
            return _forumContext.Threads.Single(t => t.ThreadId == threadId);
        }

        IEnumerable<Post> ForumRepository.GetPostsByThread(int threadId)
        {
            return (from post in _forumContext.Posts where post.ThreadId == threadId select post).ToList();
        }

        Post ForumRepository.GetPostById(int postId)
        {
            return _forumContext.Posts.Single(p => p.PostId == postId);
        }

        IEnumerable<Post> ForumRepository.FindPosts(string searchTerm)
        {
            var query = from post in _forumContext.Posts
                        where post.PostBody.Contains(searchTerm)
                        select post;
            return query.ToList();
        }
            
        User ForumRepository.GetUserById(Guid userId)
        {
            return _forumContext.Users.Single(u => u.UserId == userId);
        }

        void ForumRepository.AddForum(Forum forum)
        {

            _forumContext.Forums.Add(forum);

            _forumContext.SaveChanges();
        }

        void ForumRepository.UpdateForum(Forum forum)
        {
            var tmpForum = _forumContext.Forums.Single(f => f.ForumId == forum.ForumId);

            tmpForum.ForumTitle = forum.ForumTitle;

            _forumContext.SaveChanges();
        }

        void ForumRepository.DeleteForum(Forum forum)
        {
            var tmpForum = _forumContext.Forums.Single(f => f.ForumId == forum.ForumId); //DBSet now includes also includes Find method

            _forumContext.Forums.Remove(tmpForum);    //Must enable Cascade Delete for threads and posts

            _forumContext.SaveChanges();
        }

        void ForumRepository.AddThread(Thread thread)
        {

            _forumContext.Threads.Add(thread);

            _forumContext.SaveChanges();
        }

        void ForumRepository.UpdateThread(Thread thread)
        {
            var tmpThread = _forumContext.Threads.Single(t => t.ThreadId == thread.ThreadId);

            tmpThread.ForumId = thread.ForumId;
            tmpThread.OwnerId = thread.OwnerId;
            tmpThread.ThreadTitle = thread.ThreadTitle;

            _forumContext.SaveChanges();
        }

        void ForumRepository.DeleteThread(Thread thread)
        {
            var tmpThread = _forumContext.Threads.Single(t => t.ThreadId == thread.ThreadId); //DBSet now includes also includes Find method
            _forumContext.Threads.Remove(tmpThread);    //Must enable Cascade Delete for posts
            _forumContext.SaveChanges();
        }

        void ForumRepository.AddPost(Post post)
        {
            _forumContext.Posts.Add(post);
            _forumContext.SaveChanges();
        }

        void ForumRepository.UpdatePost(Post post)
        {
            var tmpPost = _forumContext.Posts.Single(p => p.PostId == post.PostId);

            tmpPost.PostBody = post.PostBody;
            tmpPost.PostDateTime = post.PostDateTime;
            tmpPost.PostTitle = post.PostTitle;
            tmpPost.ThreadId = post.ThreadId;
            tmpPost.UserId = post.UserId;

            _forumContext.SaveChanges();
        }

        void ForumRepository.DeletePost(Post post)
        {
            var tmpPost = _forumContext.Posts.Single(p => p.PostId == post.PostId); //DBSet now includes also includes Find method
            _forumContext.Posts.Remove(tmpPost);
            _forumContext.SaveChanges();
        }

        IEnumerable<User> ForumRepository.GetAllUsers()
        {
            return _forumContext.Users.ToList();
        } 
    }
}