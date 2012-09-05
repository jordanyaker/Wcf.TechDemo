namespace TechDemo.Data {
    using System.Transactions;

    ///<summary>
    /// Contains settings for the unit of work.
    ///</summary>
    public static class UnitOfWorkSettings {
        /// <summary>
        /// Gets the default <see cref="IsolationLevel"/>.
        /// </summary>
        public static IsolationLevel DefaultIsolation { get; set; }

        /// <summary>
        /// Gets a boolean value indicating weather to auto complete
        /// <see cref="UnitOfWorkScope"/> instances.
        /// </summary>
        public static bool AutoCompleteScope { get; set; }
    }
}