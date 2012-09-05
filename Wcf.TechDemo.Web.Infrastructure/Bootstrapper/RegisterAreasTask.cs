namespace TechDemo.Web.Bootstrapper {
    using System.Web.Mvc;
    using TechDemo.Bootstrapper;

    public class RegisterAreasTask : IBootstrapperTask {
        public void Execute() {
            // Register all areas.
            AreaRegistration.RegisterAllAreas();
        }
    }
}