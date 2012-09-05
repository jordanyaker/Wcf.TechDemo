namespace TechDemo.Wcf {
    using System;
    using System.Reflection;
    using Common.Logging;

    /// <summary>
    /// A base class for implementing service facades for interaction. 
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public abstract class ServiceFacadeBase<TService> where TService : class {
        private TService _service;
        private readonly ILog _logger = LogManager.GetLogger(typeof(TService));

        /// <summary>
        /// Gets the name of the service that the facade is interacting with.
        /// </summary>
        protected abstract string ServiceName { get; }

        /// <summary>
        /// Gets the service implementation for the facade.
        /// </summary>
        protected TService Service {
            get { return _service ?? (_service = (TService)ServiceChannelManager<TService>.GetChannel(ServiceName)); }
        }

        /// <summary>
        /// Handles the logging of errors for the service facade.
        /// </summary>
        /// <param name="exception">An exception that has occurred during the call.</param>
        /// <param name="methodBase">The method that was invokded.</param>
        protected virtual void LogError(Exception exception, MethodBase methodBase) {
            _logger.ErrorFormat("An exception occurred while executing the {0} method of the {1} service.", exception, methodBase, ServiceName);
        }
    }
}
