namespace TechDemo.Bootstrapper {
    /// <summary>
    /// An interface for all bootstrapper tasks that will be executed during the startup of an application or service.
    /// </summary>
    public interface IBootstrapperTask {
        /// <summary>
        /// The execution of the bootstrapper task.
        /// </summary>
        void Execute();
    }
}
