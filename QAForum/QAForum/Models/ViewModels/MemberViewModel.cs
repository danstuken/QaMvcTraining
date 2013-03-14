namespace QAForum.Models.ViewModels
{
    using System;

    public class MemberViewModel
    {
        public Guid MemberId { get; set; }
        public bool IsApproved { get; set; }
    }
}