namespace QAModels.Membership
{
    using System;

    public class Member
    {
        public Guid UserId { get; set; }
        public bool IsApproved { get; set; }
    }
}