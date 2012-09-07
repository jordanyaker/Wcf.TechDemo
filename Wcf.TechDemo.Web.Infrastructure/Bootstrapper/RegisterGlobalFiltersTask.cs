namespace TechDemo.Bootstrapper {
    using System.Web.Mvc;

    public class RegisterGlobalFiltersTask : IBootstrapperTask {
        private GlobalFilterCollection _filters;

        public RegisterGlobalFiltersTask()
            : this(GlobalFilters.Filters) {
        }

        public RegisterGlobalFiltersTask(GlobalFilterCollection filters) {
            this._filters = filters;
        }

        public void Execute() {
            _filters.Add(new HandleErrorAttribute());
        }
    }
}