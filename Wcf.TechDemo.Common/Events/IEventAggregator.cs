namespace TechDemo.Events {
    using System;

    /// <summary>
    /// An implementation of the Event Aggregator pattern.
    /// </summary>
    public interface IEventAggregator {
        /// <summary>
        /// Retrieves an observable event from the aggregator.
        /// </summary>
        /// <typeparam name="TEvent">The type of observable event to retrieve.</typeparam>
        /// <returns>An observable event of the specified type.</returns>
        IObservable<T> GetEvent<T>();

        /// <summary>
        /// Notifies observers for an event that something has occurred.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event that is occurring.</typeparam>
        /// <param name="value">Data that is related to the event.</param>
        void Notify<T>(T value);
    }
}
