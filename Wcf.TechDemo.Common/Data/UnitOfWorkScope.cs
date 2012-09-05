namespace TechDemo.Data {
    using System;
    using System.Transactions;
    using Common.Logging;

    /// <summary>
    /// Helper class that allows starting and using a unit of work like:
    /// <![CDATA[
    ///     using (UnitOfWorkScope scope = new UnitOfWorkScope()) {
    ///         //Do some stuff here.
    ///         scope.Commit();
    ///     }
    /// 
    /// ]]>
    /// </summary>
    public class UnitOfWorkScope : Disposable, IUnitOfWorkScope {
        bool _commitAttempted;
        bool _completed;
        readonly Guid _scopeId = Guid.NewGuid();
        readonly ILog _logger = LogManager.GetLogger<UnitOfWorkScope>();

        /// <summary>
        /// Event fired when the scope is comitting.
        /// </summary>
        public event Action<IUnitOfWorkScope> ScopeComitting;

        /// <summary>
        /// Event fired when the scope is rollingback.
        /// </summary>
        public event Action<IUnitOfWorkScope> ScopeRollingback;

        /// <summary>
        /// Default Constuctor.
        /// Creates a new <see cref="UnitOfWorkScope"/> with the <see cref="System.Data.IsolationLevel.Serializable"/> 
        /// transaction isolation level.
        /// </summary>
        public UnitOfWorkScope() : this(TransactionMode.Default) { }

        /// <summary>
        /// Overloaded Constructor.
        /// Creates a new instance of the <see cref="UnitOfWorkScope"/> class.
        /// </summary>
        /// <param name="mode">A <see cref="TransactionMode"/> enum specifying the transation mode
        /// of the unit of work.</param>
        public UnitOfWorkScope(TransactionMode mode) {
            UnitOfWorkManager.CurrentTransactionManager.EnlistScope(this, mode);
        }

        /// <summary>
        /// Gets the unique Id of the <see cref="UnitOfWorkScope"/>.
        /// </summary>
        /// <value>A <see cref="Guid"/> representing the unique Id of the scope.</value>
        public Guid ScopeId {
            get { return _scopeId; }
        }

        /// <summary>
        /// Gets the current unit of work that the scope participates in.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> to retrieve.</typeparam>
        /// <returns>A <see cref="IUnitOfWork"/> instance of type <typeparamref name="T"/> that
        /// the scope participates in.</returns>
        public T CurrentUnitOfWork<T>() {
            var currentUow = UnitOfWorkManager.CurrentUnitOfWork;

            return (T)currentUow;
        }

        ///<summary>
        /// Commits the current running transaction in the scope.
        ///</summary>
        public void Commit() {
            _commitAttempted = true;

            OnCommit();
        }

        /// <summary>
        /// Marks the scope as completed.
        /// Used internally and should not be used by consumers.
        /// </summary>
        public void Complete() {
            _completed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        void OnCommit() {
            _logger.Info(x => x("UnitOfWorkScope {0} Comitting.", _scopeId));

            if (ScopeComitting != null) {
                ScopeComitting(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void OnRollback() {
            _logger.Info(x => x("UnitOfWorkScope {0} Rolling back.", _scopeId));

            if (ScopeRollingback != null) {
                ScopeRollingback(this);
            }
        }

        protected override void OnDisposing() {
            try {
                if (_completed) {
                    return;
                }

                if (!_commitAttempted && UnitOfWorkSettings.AutoCompleteScope) {
                    //Scope did not try to commit before, and auto complete is switched on. Trying to commit.
                    //If an exception occurs here, the finally block will clean things up for us.
                    OnCommit();
                } else {
                    //Scope either tried a commit before or auto complete is turned off. Trying to rollback.
                    //If an exception occurs here, the finally block will clean things up for us.
                    OnRollback();
                }
            } finally {
                ScopeComitting = null;
                ScopeRollingback = null;
            }
        }
    }
}
