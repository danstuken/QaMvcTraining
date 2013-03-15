namespace QAForum.Controllers
{
    using System.Web.Mvc;

    public class LegacyController: Controller
    {
         public ActionResult Stats()
         {
             return Redirect("/Legacy/Stats.aspx");
         }
    }
}