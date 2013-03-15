namespace QAForum.Tests.Mocks
{
    using System.Security.Principal;
    using System.Web;

    public class MockHttpContext: HttpContextBase
    {
        public override IPrincipal User { get; set; }
    }
}