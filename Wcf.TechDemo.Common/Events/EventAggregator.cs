namespace TechDemo.Events {
    using System;
    using System.Collections.Concurrent;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    /// <summary>
    /// The default implementation of the <see cref="IEventAggregator"/> interface.
    /// </summary>
    public class EventAggregator : IEventAggregator {
        private readonly ConcurrentDictionary<Type, object> _subjects = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Retrieves an observable event from the aggregator.
        /// </summary>
        /// <typeparam name="TEvent">The type of observable event to retrieve.</typeparam>
        /// <returns>An observable event of the specified type.</returns>
        public IObservable<TEvent> GetEvent<TEvent>() {
            var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());

            return subject.AsObservable();
        }

        /// <summary>
        /// Notifies observers for an event that something has occurred.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event that is occurring.</typeparam>
        /// <param name="value">Data that is related to the event.</param>
        public void Notify<TEvent>(TEvent value) {
            object subject;

            if (_subjects.TryGetValue(typeof(TEvent), out subject)) {
                ((ISubject<TEvent>)subject).OnNext(value);
            }
        }
    }
}
