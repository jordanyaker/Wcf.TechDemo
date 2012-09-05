namespace TechDemo {
    /// <summary>
    /// Contains useful extensions for working with objects of type <typeparamref name="System.Object"/>.
    /// </summary>
    public static class ObjectExtensions {
        ///<summary>
        /// Builds a key that from the full name of the type and the supplied user key.
        ///</summary>
        ///<param name="userKey">The user supplied key, if any.</param>
        ///<typeparam name="T">The type for which the key is built.</typeparam>
        ///<returns>string.</returns>
        public static string BuildFullKey<T>(this object userKey) {
            if (userKey == null)
                return typeof(T).FullName;
            return typeof(T).FullName + userKey;
        }
    }
}
