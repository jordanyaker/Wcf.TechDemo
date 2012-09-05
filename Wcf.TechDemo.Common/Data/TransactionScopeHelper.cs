namespace TechDemo.Data {
    using System;
    using System.Transactions;
    using Common.Logging;

    /// <summary>
    /// Helper class to create <see cref="TransactionScope"/> instances.
    /// </summary>
    public static class TransactionScopeHelper {
        static readonly ILog Logger = LogManager.GetLogger(typeof(TransactionScopeHelper));

        ///<summary>
        /// Create a new <typeparamref name="TransactionScope"/> using the supplied parameters.
        ///</summary>
        ///<param name="isolationLevel"></param>
        ///<param name="txMode"></param>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        public static TransactionScope CreateScope(IsolationLevel isolationLevel, TransactionMode txMode) {
            if (txMode == TransactionMode.New) {
                Logger.Debug(x => x("Creating a new TransactionScope with TransactionScopeOption.RequiresNew"));
                return new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = isolationLevel });
            }
            if (txMode == TransactionMode.Supress) {
                Logger.Debug(x => x("Creating a new TransactionScope with TransactionScopeOption.Supress"));
                return new TransactionScope(TransactionScopeOption.Suppress);
            }
            Logger.Debug(x => x("Creating a new TransactionScope with TransactionScopeOption.Required"));
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = isolationLevel });
        }
    }
}