namespace QAForum.Legacy
{
    using System;
    using System.Web;
    using Autofac.Integration.Web;
    using Autofac;
    using Providers;

    public partial class Stats : System.Web.UI.Page
    {
        public ForumProvider ForumProvider { get; set; }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            var cpa = (IContainerProviderAccessor) HttpContext.Current.ApplicationInstance;
            var cp = cpa.ContainerProvider;
            cp.RequestLifetime.InjectProperties(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var result = ForumProvider.GetPostingStatistics();
            foreach (var r in result)
                ForumPostsChart.Series[0].Points.AddXY(r.ForumTitle, r.NumberOfPosts);
        }
    }
}