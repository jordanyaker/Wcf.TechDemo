namespace TechDemo.Configuration {
    using System;
    using TechDemo.Context;
    using TechDemo.IoC;

    ///<summary>
    /// Default implementation of <see cref="ICommonConfig"/> class.
    ///</summary>
    public class CommonConfig : ICommonConfig {
        readonly IContainerAdapter _containerAdapter;

        ///<summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="CommonConfig"/>  class.
        ///</summary>
        ///<param name="containerAdapter">An instance of <see cref="IContainerAdapter"/> that can be
        /// used to register components.</param>
        public CommonConfig(IContainerAdapter containerAdapter) {
            _containerAdapter = containerAdapter;
            InitializeDefaults();
        }

        /// <summary>
        /// Registers default components.
        /// </summary>
        void InitializeDefaults() {
            _containerAdapter.Register<IContext, Context.Default.Context>();
        }

        /// <summary>
        /// Configure state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed.
        /// </typeparam>
        /// <returns><see cref="ICommonConfig"/></returns>
        public ICommonConfig ConfigureState<T>() where T : IStateConfiguration, new() {
            var configuration = (T)Activator.CreateInstance(typeof(T));
            configuration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configure state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed.
        /// </typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IStateConfiguration"/> instance.</param>
        /// <returns><see cref="ICommonConfig"/></returns>
        public ICommonConfig ConfigureState<T>(Action<T> actions) where T : IStateConfiguration, new() {
            var configuration = (T)Activator.CreateInstance(typeof(T));
            actions(configuration);
            configuration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configure data providers used.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers.</typeparam>
        /// <returns><see cref="ICommonConfig"/></returns>
        public ICommonConfig ConfigureData<T>() where T : IDataConfiguration, new() {
            var datConfiguration = (T)Activator.CreateInstance(typeof(T));
            datConfiguration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configure data providers used.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers.</typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IDataConfiguration"/> instance.</param>
        /// <returns><see cref="ICommonConfig"/></returns>
        public ICommonConfig ConfigureData<T>(Action<T> actions) where T : IDataConfiguration, new() {
            var dataConfiguration = (T)Activator.CreateInstance(typeof(T));
            actions(dataConfiguration);
            dataConfiguration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configures unit of work settings.
        /// </summary>
        /// <typeparam name="T">A <see cref="IUnitOfWorkConfiguration"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        /// <returns><see cref="ICommonConfig"/></returns>
        public ICommonConfig ConfigureUnitOfWork<T>() where T : IUnitOfWorkConfiguration, new() {
            var uowConfiguration = (T)Activator.CreateInstance(typeof(T));
            uowConfiguration.Configure(_containerAdapter);
            return this;
        }

        ///<summary>
        /// Configures unit of work settings.
        ///</summary>
        /// <typeparam name="T">A <see cref="ICommonConfig"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        ///<param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IUnitOfWorkConfiguration"/> instance.</param>
        ///<returns><see cref="ICommonConfig"/></returns>
        public ICommonConfig ConfigureUnitOfWork<T>(Action<T> actions) where T : IUnitOfWorkConfiguration, new() {
            var uowConfiguration = (T)Activator.CreateInstance(typeof(T));
            actions(uowConfiguration);
            uowConfiguration.Configure(_containerAdapter);
            return this;
        }
    }
}
