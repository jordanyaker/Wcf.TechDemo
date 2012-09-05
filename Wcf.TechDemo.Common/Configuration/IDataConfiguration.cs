namespace TechDemo.Configuration {
    using TechDemo.IoC;

    /// <summary>
    /// Base interface implemented by specific data configurators that configure data providers.
    /// </summary>
    public interface IDataConfiguration {
        /// <summary>
        /// Called <see cref="Configure"/> to configure data providers.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance that allows
        /// registering components.</param>
        void Configure(IContainerAdapter containerAdapter);
    }
}
