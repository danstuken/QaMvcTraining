namespace QAForum.Tests.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcContrib.TestHelper;
    using NSubstitute;
    using Providers;
    using QAForum.Controllers;

    [TestClass]
    public class ForumControllersTests
    {
        private ForumController _controller;

        [TestInitialize]
        public void Initialise()
        {
            var mockForumProvider = Substitute.For<ForumProvider>();
            _controller = new ForumController(mockForumProvider);
        }

        [TestMethod]
        public void ForumController_Index_Get_ReturnsIndexView()
        {
            var view = (ViewResult)_controller.Index();

            view.AssertViewRendered();
        }

        [TestMethod]
        public void ForumController_Create_Get_ProvidesModelToView()
        {
            var view = (ViewResult) _controller.Create();

            view.Model.ShouldNotBeNull("Forum model for Forum Create is null");
        }
    }
}