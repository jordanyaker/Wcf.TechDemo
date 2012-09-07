namespace TechDemo.Bootstrapper {
    using System.Web.Mvc;

    public class RegisterAreasTask : IBootstrapperTask {
        public void Execute() {
            // Register all areas.
            AreaRegistration.RegisterAllAreas();
        }
    }
}