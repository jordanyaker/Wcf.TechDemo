namespace TechDemo.Wcf {
    using System.Collections.Generic;
    using System.ServiceModel;

    /// <summary>
    /// Manages the collection of <see cref="ChannelFactory"/> objects for the current process.
    /// </summary>
    internal sealed class ChannelStore {
        private static volatile ChannelStore _instance;
        private static readonly object SyncRoot = new object();
        public Dictionary<string, ChannelFactory> ProxyCollection = new Dictionary<string, ChannelFactory>();

        /// <summary>
        /// Private constructore to override the default constructor.
        /// </summary>
        private ChannelStore() {
        }

        /// <summary>
        /// Retrieves the current instance of the <typeparamref name="ChannelStore"/> class.
        /// </summary>
        public static ChannelStore Current {
            get {
                if (_instance == null) {
                    lock (SyncRoot) {
                        if (_instance == null) {
                            _instance = new ChannelStore();
                        }
                    }
                }
                return _instance;
            }
        }
    }

}
