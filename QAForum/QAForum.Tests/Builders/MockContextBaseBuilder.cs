namespace QAForum.Tests.Builders
{
    using System.Security.Principal;
    using System.Web;
    using Mocks;

    public class MockContextBaseBuilder
    {
        private IPrincipal _user = new MockUserBuilder().Build();

        public MockContextBaseBuilder WithUser(IPrincipal user)
        {
            _user = user;
            return this;
        }

        public HttpContextBase Build()
        {
            return new MockHttpContext
                {
                    User = _user
                };
        }
    }
}