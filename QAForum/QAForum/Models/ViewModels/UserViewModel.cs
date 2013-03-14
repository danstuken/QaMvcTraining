namespace QAForum.Models.ViewModels
{
    using System;

    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AvatarPath { get; set; }
    }
}