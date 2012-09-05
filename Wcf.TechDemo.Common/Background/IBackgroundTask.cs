namespace TechDemo.Background {
    /// <summary>
    /// An interface for executing background tasks and processing.
    /// </summary>
    public interface IBackgroundTask {
        /// <summary>
        /// Gets the current state of the background task.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Starts the background task.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the background task from running.
        /// </summary>
        void Stop();
    }
}
