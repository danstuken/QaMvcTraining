namespace QAForum.Filters
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Diagnostics;

    public class DiagnosticsFilterProvider : AutofacFilterAttributeFilterProvider
    {
        private readonly IList<ControllerAction> _permittedActions = new List<ControllerAction>();

        public void Add(string controllerName, string actionName)
        {
            _permittedActions.Add(new ControllerAction
                {
                    ControllerName = controllerName,
                    ActionName = actionName
                });
        }

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (var controllerAction in _permittedActions)
            {
                if ((controllerAction.ControllerName == actionDescriptor.ControllerDescriptor.ControllerName
                     || controllerAction.ControllerName == "*")
                    && (controllerAction.ActionName == actionDescriptor.ActionName
                        || controllerAction.ActionName == "*"))
                {
                    var filter = new Filter(new DiagnosticFilter(), FilterScope.First, null);
                    AutofacDependencyResolver.Current.RequestLifetimeScope.InjectProperties(filter.Instance);
                    yield return filter;
                    break;
                }
            }
            yield break;
        }
    }

    internal class ControllerAction
    {
        internal string ControllerName { get; set; }
        internal string ActionName { get; set; }
    }
}