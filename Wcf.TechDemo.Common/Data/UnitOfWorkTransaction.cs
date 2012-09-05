namespace TechDemo.Data {
    using System;
    using System.Collections.Generic;
    using System.Transactions;
    using Common.Logging;

    /// <summary>
    /// Encapsulates a unit of work transaction.
    /// </summary>
    public class UnitOfWorkTransaction : Disposable {
        private TransactionScope _transaction;
        private IUnitOfWork _unitOfWork;
        private IList<IUnitOfWorkScope> _attachedScopes = new List<IUnitOfWorkScope>();

        private readonly Guid _transactionId = Guid.NewGuid();
        private readonly ILog _logger = LogManager.GetLogger<UnitOfWorkTransaction>();


        ///<summary>
        /// Raised when the transaction is disposing.
        ///</summary>
        public event Action<UnitOfWorkTransaction> TransactionDisposing;

        ///<summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="UnitOfWorkTransaction"/> class.
        ///</summary>
        ///<param name="unitOfWork">The <see cref="IUnitOfWork"/> instance managed by the 
        /// <see cref="UnitOfWorkTransaction"/> instance.</param>
        ///<param name="transaction">The <see cref="TransactionScope"/> instance managed by 
        /// the <see cref="UnitOfWorkTransaction"/> instance.</param>
        public UnitOfWorkTransaction(IUnitOfWork unitOfWork, TransactionScope transaction) {
            _unitOfWork = unitOfWork;
            _transaction = transaction;
            _logger.Info(x => x("New UnitOfWorkTransction created with Id {0}", _transactionId));
        }

        ///<summary>
        /// Gets the unique transaction id of the <see cref="UnitOfWorkTransaction"/> instance.
        ///</summary>
        /// <value>A <see cref="Guid"/> representing the unique id of the <see cref="UnitOfWorkTransaction"/> instance.</value>
        public Guid TransactionId {
            get { return _transactionId; }
        }

        /// <summary>
        /// Gets the <see cref="IUnitOfWork"/> instance managed by the 
        /// <see cref="UnitOfWorkTransaction"/> instance.
        /// </summary>
        public IUnitOfWork UnitOfWork {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// Attaches a <see cref="UnitOfWorkScope"/> instance to the 
        /// <see cref="UnitOfWorkTransaction"/> instance.
        /// </summary>
        /// <param name="scope">The <see cref="UnitOfWorkScope"/> instance to attach.</param>
        public void EnlistScope(IUnitOfWorkScope scope) {
            _logger.Info(x => x("Scope {1} enlisted with transaction {1}", scope.ScopeId, _transactionId));
            _attachedScopes.Add(scope);
            scope.ScopeComitting += OnScopeCommitting;
            scope.ScopeRollingback += OnScopeRollingBack;
        }

        /// <summary>
        /// Callback executed when an enlisted scope has comitted.
        /// </summary>
        void OnScopeCommitting(IUnitOfWorkScope scope) {
            _logger.Info(x => x("Commit signalled by scope {0} on transaction {1}.", scope.ScopeId, _transactionId));

            if (!_attachedScopes.Contains(scope)) {
                Dispose();
                throw new InvalidOperationException("The scope being comitted is not attached to the current transaction.");
            }
            
            scope.ScopeComitting -= OnScopeCommitting;
            scope.ScopeRollingback -= OnScopeRollingBack;
            scope.Complete();
            
            _attachedScopes.Remove(scope);
            if (_attachedScopes.Count == 0) {
                _logger.Info(x => x("All scopes have signalled a commit on transaction {0}. Flushing unit of work and comitting attached TransactionScope.", _transactionId));
            
                try {
                    _unitOfWork.Flush();
                    _transaction.Complete();
                } finally {
                    Dispose(); //Dispose the transaction after comitting.
                }
            }
        }

        /// <summary>
        /// Callback executed when an enlisted scope is rolledback.
        /// </summary>
        void OnScopeRollingBack(IUnitOfWorkScope scope) {
            _logger.Info(x => x("Rollback signalled by scope {0} on transaction {1}.", scope.ScopeId, _transactionId));
            _logger.Info(x => x("Detaching all scopes and disposing of attached TransactionScope on transaction {0}", _transactionId));

            scope.ScopeComitting -= OnScopeCommitting;
            scope.ScopeRollingback -= OnScopeRollingBack;
            scope.Complete();

            _attachedScopes.Remove(scope);
            Dispose();
        }

        protected override void OnDisposing() {
            _logger.Info(x => x("Disposing off transction {0}", _transactionId));
            if (_unitOfWork != null) {
                _unitOfWork.Dispose();
                _unitOfWork = null;
            }

            if (_transaction != null) {
                _transaction.Dispose();
                _transaction = null;
            }

            if (TransactionDisposing != null) {
                TransactionDisposing(this);
                TransactionDisposing = null;
            }

            if (_attachedScopes != null && _attachedScopes.Count > 0) {
                _attachedScopes.ForEach(scope => {
                    scope.ScopeComitting -= OnScopeCommitting;
                    scope.ScopeRollingback -= OnScopeRollingBack;
                    scope.Complete();
                });

                _attachedScopes.Clear();
                _attachedScopes = null;
            }
        }
    }
}