namespace TechDemo.Data {
    using System;
    using Common.Logging;
    using Microsoft.Practices.ServiceLocation;
    using TechDemo.State;

    ///<summary>
    /// Gets an instances of <see cref="ITransactionManager"/>.
    ///</summary>
    public static class UnitOfWorkManager {
        private static Func<ITransactionManager> _provider;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWorkManager));
        private const string LocalTransactionManagerKey = "UnitOfWorkManager.LocalTransactionManager";
        private static readonly Func<ITransactionManager> DefaultTransactionManager = () => {
            Logger.Debug(x => x("Using default UnitOfWorkManager provider to resolve current transaction manager."));

            var state = ServiceLocator.Current.GetInstance<IState>();
            
            var transactionManager = state.Local.Get<ITransactionManager>(LocalTransactionManagerKey);
            if (transactionManager == null) {
                Logger.Debug(x => x("No valid ITransactionManager found in Local state. Creating a new TransactionManager."));
            
                transactionManager = new TransactionManager();
                state.Local.Put(LocalTransactionManagerKey, transactionManager);
            }
          
            return transactionManager;
        };

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="UnitOfWorkManager"/>.
        /// </summary>
        static UnitOfWorkManager() {
            _provider = DefaultTransactionManager;
        }

        ///<summary>
        /// Sets a <see cref="Func{T}"/> of <see cref="ITransactionManager"/> that the 
        /// <see cref="UnitOfWorkManager"/> uses to get an instance of <see cref="ITransactionManager"/>
        ///</summary>
        ///<param name="provider"></param>
        public static void SetTransactionManagerProvider(Func<ITransactionManager> provider) {
            if (provider == null) {
                Logger.Debug(x => x("The transaction manager provide is being set to null. Using " +
                                    " the transaction manager to the default transaction manager provider."));
                _provider = DefaultTransactionManager;
                return;
            }
            Logger.Debug(x => x("The transaction manager provider is being overriden. Using supplied" +
                                " trasaction manager provider."));
            _provider = provider;
        }

        /// <summary>
        /// Gets the current <see cref="ITransactionManager"/>.
        /// </summary>
        public static ITransactionManager CurrentTransactionManager {
            get {
                return _provider();
            }
        }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> instance.
        /// </summary>
        public static IUnitOfWork CurrentUnitOfWork {
            get {
                return _provider().CurrentUnitOfWork;
            }
        }
    }
}