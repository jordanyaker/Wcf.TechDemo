namespace TechDemo.Configuration {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Fluent configuration interface exposed to configure different services exposed.
    /// </summary>
    public interface ICommonConfig {
        /// <summary>
        /// Configure state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed.
        /// </typeparam>
        /// <returns><see cref="ICommonConfig"/></returns>
        ICommonConfig ConfigureState<T>() where T : IStateConfiguration, new();

        /// <summary>
        /// Configure state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed.
        /// </typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IStateConfiguration"/> instance.</param>
        /// <returns><see cref="ICommonConfig"/></returns>
        ICommonConfig ConfigureState<T>(Action<T> actions) where T : IStateConfiguration, new();

        /// <summary>
        /// Configure data providers used.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers.</typeparam>
        /// <returns><see cref="ICommonConfig"/></returns>
        ICommonConfig ConfigureData<T>() where T : IDataConfiguration, new();

        /// <summary>
        /// Configure data providers used.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers.</typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IDataConfiguration"/> instance.</param>
        /// <returns><see cref="ICommonConfig"/></returns>
        ICommonConfig ConfigureData<T>(Action<T> actions) where T : IDataConfiguration, new();

        /// <summary>
        /// Configures unit of work settings.
        /// </summary>
        /// <typeparam name="T">A <see cref="IUnitOfWorkConfiguration"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        /// <returns><see cref="ICommonConfig"/></returns>
        ICommonConfig ConfigureUnitOfWork<T>() where T : IUnitOfWorkConfiguration, new();

        ///<summary>
        /// Configures unit of work settings.
        ///</summary>
        /// <typeparam name="T">A <see cref="ICommonConfig"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        ///<param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IUnitOfWorkConfiguration"/> instance.</param>
        ///<returns><see cref="ICommonConfig"/></returns>
        ICommonConfig ConfigureUnitOfWork<T>(Action<T> actions) where T : IUnitOfWorkConfiguration, new();
    }

}
