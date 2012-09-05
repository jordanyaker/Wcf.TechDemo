namespace TechDemo.Wcf {
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;

    /// <summary>
    /// Provides management capabilities for objects of 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ServiceChannelManager<T> where T : class {
        private static readonly ReaderWriterLockSlim CollectionLock;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ServiceChannelManager() {
            CollectionLock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// Causes the channel to terminate communication immediately and transition to the closed state.
        /// </summary>
        /// <param name="channel">The channel to be closed.</param>
        public static void Abort(T channel) {
            if (channel != null) {
                ((ICommunicationObject)channel).Abort();
            }
        }

        /// <summary>
        /// Causes the channel to transition to the closes state.
        /// </summary>
        /// <param name="channel">The channel to be closed.</param>
        public static void Close(T channel) {
            if (channel != null) {
                ((ICommunicationObject)channel).Close();
            }
        }

        /// <summary>
        /// Retrieve a new channel instance using the specified endopoint name.
        /// </summary>
        /// <param name="endpointName">The name of the end point to retrieve a channel for.</param>
        /// <returns>Returns a channel for the endpoint with the supplied name.  If an initialized channel could not be found, a new one will be utilized.</returns>
        public static IChannel GetChannel(string endpointName) {
            ChannelFactory<T> factory = null;
            
            CollectionLock.EnterUpgradeableReadLock();
            try {
                if (ChannelStore.Current.ProxyCollection.Keys.Contains(endpointName)) {
                    factory = (ChannelFactory<T>)ChannelStore.Current.ProxyCollection[endpointName];
                } else {
                    factory = new ChannelFactory<T>(endpointName);
            
                    CollectionLock.EnterWriteLock();
                    try {
                        ChannelStore.Current.ProxyCollection.Add(endpointName, factory);
                    } finally {
                        CollectionLock.ExitWriteLock();
                    }
                }
            } finally {
                CollectionLock.ExitUpgradeableReadLock();
            }

            return (IChannel)factory.CreateChannel();
        }
    }

}
