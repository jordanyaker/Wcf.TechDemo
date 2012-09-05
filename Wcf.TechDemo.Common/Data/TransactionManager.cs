namespace TechDemo.Data {
    using System;
    using System.Collections.Generic;
    using Common.Logging;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Default implementation of <see cref="ITransactionManager"/> interface.
    /// </summary>
    public class TransactionManager : Disposable, ITransactionManager {
        private readonly Guid _transactionManagerId = Guid.NewGuid();
        private readonly ILog _logger = LogManager.GetLogger<TransactionManager>();
        private readonly LinkedList<UnitOfWorkTransaction> _transactions = new LinkedList<UnitOfWorkTransaction>();

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="TransactionManager"/> class.
        /// </summary>
        public TransactionManager() {
            _logger.Debug(x => x("New instance of TransactionManager with Id {0} created.", _transactionManagerId));
        }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> instance.
        /// </summary>
        public IUnitOfWork CurrentUnitOfWork {
            get {
                return CurrentTransaction == null ? null : CurrentTransaction.UnitOfWork;
            }
        }

        /// <summary>
        /// Gets the current <see cref="UnitOfWorkTransaction"/> instance.
        /// </summary>
        public UnitOfWorkTransaction CurrentTransaction {
            get {
                return _transactions.Count == 0 ? null : _transactions.First.Value;
            }
        }

        /// <summary>
        /// Enlists a <see cref="UnitOfWorkScope"/> instance with the transaction manager,
        /// with the specified transaction mode.
        /// </summary>
        /// <param name="scope">The <see cref="IUnitOfWorkScope"/> to register.</param>
        /// <param name="mode">A <see cref="TransactionMode"/> enum specifying the transaciton
        /// mode of the unit of work.</param>
        public void EnlistScope(IUnitOfWorkScope scope, TransactionMode mode) {
            _logger.Info(x =>
                x("Enlisting scope {0} with transaction manager {1} with transaction mode {2}",
                    scope.ScopeId,
                    _transactionManagerId,
                    mode)
            );

            if (_transactions.Count == 0 || mode == TransactionMode.New || mode == TransactionMode.Supress) {
                _logger.Debug(x => x("Enlisting scope {0} with mode {1} requires a new TransactionScope to be created.", scope.ScopeId, mode));

                var uowFactory = ServiceLocator.Current.GetInstance<IUnitOfWorkFactory>();
                var unitOfWork = uowFactory.Create();

                var txScope = TransactionScopeHelper.CreateScope(UnitOfWorkSettings.DefaultIsolation, mode);
                var transaction = new UnitOfWorkTransaction(unitOfWork, txScope);
                transaction.TransactionDisposing += OnTransactionDisposing;
                transaction.EnlistScope(scope);

                _transactions.AddFirst(transaction);
            } else {
                CurrentTransaction.EnlistScope(scope);
            }
        }

        /// <summary>
        /// Handles a Dispose signal from a transaction.
        /// </summary>
        /// <param name="transaction"></param>
        void OnTransactionDisposing(UnitOfWorkTransaction transaction) {
            _logger.Info(x => x("UnitOfWorkTransaction {0} signalled a disposed. Unregistering transaction from TransactionManager {1}",
                                    transaction.TransactionId, _transactionManagerId));

            transaction.TransactionDisposing -= OnTransactionDisposing;
            var node = _transactions.Find(transaction);
            if (node != null)
                _transactions.Remove(node);
        }

        protected override void OnDisposing() {
            _logger.Info(x => x("Disposing off transction manager {0}", _transactionManagerId));
            if (_transactions != null && _transactions.Count > 0) {
                _transactions.ForEach(tx => {
                    tx.TransactionDisposing -= OnTransactionDisposing;
                    tx.Dispose();
                });
                _transactions.Clear();
            }
        }
    }
}