namespace QAForum.Tests.Builders
{
    using System.Collections.Generic;
    using System.Security.Principal;

    public class MockUserBuilder
    {
        private string _username = string.Empty;
        private readonly List<string> _roles = new List<string>();

        public MockUserBuilder WithUserName(string username)
        {
            _username = username;
            return this;
        }

        public MockUserBuilder WithRole(string role)
        {
            _roles.Add(role);
            return this;
        }

        public IPrincipal Build()
        {
            return new GenericPrincipal(new GenericIdentity(_username), _roles.ToArray());
        }
    }
}