namespace TechDemo.Bootstrapper {
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RegisterRoutesTask : IBootstrapperTask {
        private readonly RouteCollection _routes;

        /// <summary>
        /// Constructor for the <see cref="RegisterRoutesTask"/> using a supplied collection of routes.
        /// </summary>
        /// <param name="routes">The collection of routes to use for the application.</param>
        public RegisterRoutesTask(RouteCollection routes) {
            _routes = routes;
        }
        
        /// <summary>
        /// Default constructor for the <see cref="RegisterRoutesTask"/>.
        /// </summary>
        public RegisterRoutesTask()
            : this(RouteTable.Routes) {
        }

        /// <summary>
        /// The execution of the bootstrapper task.
        /// </summary>
        public void Execute() {
            // Turns off the unnecessary file exists check
            _routes.RouteExistingFiles = true;

            // Ignore axd files such as assest, image, sitemap etc
            _routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Exclude favicon (google toolbar request gif file as fav icon)
            _routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            // Ignore the content directories which contains images, scripts, & css
            _routes.IgnoreRoute("Content/{*pathInfo}");
            _routes.IgnoreRoute("Scripts/{*pathInfo}");

            // Ignore status, html, xml files.
            _routes.IgnoreRoute("{file}.ashx");
            _routes.IgnoreRoute("{file}.txt");
            _routes.IgnoreRoute("{file}.htm");
            _routes.IgnoreRoute("{file}.html");
            _routes.IgnoreRoute("{file}.xml");

            // Register the routes for the application.
            _routes.MapRoute("default", "{controller}/{action}", new { controller = "Public", action = "Landing" });
        }
    }
}