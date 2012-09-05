namespace TechDemo {
    using TechDemo.Configuration;
    using TechDemo.IoC;

    /// <summary>
    /// Static configuration class that allows configuration of common services.
    /// </summary>
    public static class Configure {
        /// <summary>
        /// Entry point to the application configuration.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance to use
        /// for component registration.</param>
        /// <returns>An instance of <see cref="ICommonConfig"/> that can be used to configure
        /// the aoplication configuration.</returns> 
        public static ICommonConfig Using(IContainerAdapter containerAdapter) {
            return new CommonConfig(containerAdapter);
        }
    }
}
