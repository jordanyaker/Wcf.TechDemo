namespace TechDemo.Background {
    /// <summary>
    /// Base implementation of the <see cref="IBackgroundTask"/> interface. 
    /// </summary>
    public abstract class BackgroundTask : IBackgroundTask {
        /// <summary>
        /// Gets the current state of the background task.
        /// </summary>
        public bool IsRunning { get; private set; }
       
        /// <summary>
        /// Starts the background task.
        /// </summary>
        public void Start() {
            IsRunning = true;
            OnStart();
        }

        /// <summary>
        /// Stops the background task from running.
        /// </summary>
        public void Stop() {
            OnStop();
            IsRunning = false;
        }

        /// <summary>
        /// Executes upon the starting of the background task.
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Executes upon the stopping of the background task.
        /// </summary>
        protected abstract void OnStop();
    }
}
