namespace TechDemo.Data.EntityFramework {
    using System;
    using System.Data.Objects;

    /// <summary>
    /// Implements the <see cref="IUnitOfWorkFactory"/> interface to provide an implementation of a factory
    /// that creates <see cref="EFUnitOfWork"/> instances.
    /// </summary>
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory {
        EFSessionResolver _resolver = new EFSessionResolver();

        /// Registers a <see cref="Func{T}"/> of type <see cref="ObjectContext"/> provider that can be used
        /// to resolve instances of <see cref="ObjectContext"/>.
        /// </summary>
        /// <param name="contextProvider">A <see cref="Func{T}"/> of type <see cref="ObjectContext"/>.</param>
        public void RegisterObjectContextProvider(Func<ObjectContext> contextProvider) {
            _resolver.RegisterObjectContextProvider(contextProvider);
        }

        /// <summary>
        /// Creates a new instance of <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <returns>Instances of <see cref="EFUnitOfWork"/>.</returns>
        public IUnitOfWork Create() {
            return new EFUnitOfWork(_resolver);
        }
    }
}
