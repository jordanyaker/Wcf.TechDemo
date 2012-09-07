namespace TechDemo.Bootstrapper {
    using System;
    using Microsoft.Practices.ServiceLocation;
    using System.Configuration;
    using TechDemo.IoC;

    /// <summary>
    /// A bootstrapper utility class that will handle the execution of startup tasks.
    /// </summary>
    public static class BootstrapManager {
        /// <summary>
        /// Executes the loading of all <see cref="IBootStrapperTask"/> implementations that have been registered.
        /// </summary>
        public static void Run() {
            ServiceLocator.Current.GetAllInstances<IBootstrapperTask>().ForEach(t => t.Execute());
        }
    }
}
