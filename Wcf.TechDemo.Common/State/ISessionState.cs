namespace TechDemo.State {
    /// <summary>
    /// Interface implemented by session state providers that store and retrieve state data for the current executing session.
    /// </summary>
    public interface ISessionState {
        /// <summary>
        /// Gets state data stored with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        T Get<T>();

        /// <summary>
        /// Gets state data stored with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        T Get<T>(object key);

        /// <summary>
        /// Puts state data into the session state with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        void Put<T>(T instance);

        /// <summary>
        /// Puts state data into the session state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        void Put<T>(object key, T instance);

        /// <summary>
        /// Removes state data stored in the session state with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        void Remove<T>();

        /// <summary>
        /// Removes state data stored in the session state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        void Remove<T>(object key);

        /// <summary>
        /// Clears all state data stored in the session.
        /// </summary>
        void Clear();
    }
}