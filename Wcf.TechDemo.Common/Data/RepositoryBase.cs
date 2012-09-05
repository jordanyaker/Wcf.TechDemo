namespace TechDemo.Data {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Practices.ServiceLocation;
    using TechDemo.Specifications;

    ///<summary>
    /// A base class for implementors of <see cref="IRepository{TEntity}"/>.
    ///</summary>
    ///<typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> {
        /// <summary>
        /// Gets the <see cref="IQueryable{TEntity}"/> used by the <see cref="RepositoryBase{TEntity}"/> 
        /// to execute Linq queries.
        /// </summary>
        /// <value>A <see cref="IQueryable{TEntity}"/> instance.</value>
        /// <remarks>
        /// Inheritors of this base class should return a valid non-null <see cref="IQueryable{TEntity}"/> instance.
        /// </remarks>
        protected abstract IQueryable<TEntity> RepositoryQuery { get; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{TEntity}" /> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<TEntity> GetEnumerator() {
            return RepositoryQuery.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return RepositoryQuery.GetEnumerator();
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="IQueryable" />.
        /// </summary>
        /// <returns>
        /// The <see cref="Expression" /> that is associated with this instance of <see cref="IQueryable" />.
        /// </returns>
        public Expression Expression {
            get { return RepositoryQuery.Expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="IQueryable" /> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType {
            get { return RepositoryQuery.ElementType; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryProvider" /> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider {
            get { return RepositoryQuery.Provider; }
        }

        /// <summary>
        /// Gets the a <see cref="IUnitOfWork"/> of <typeparamref name="T"/> that
        /// the repository will use to query the underlying store.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> implementation to retrieve.</typeparam>
        /// <returns>The <see cref="IUnitOfWork"/> implementation.</returns>
        public virtual T UnitOfWork<T>() where T : IUnitOfWork {
            var currentScope = UnitOfWorkManager.CurrentUnitOfWork;
            return ((T)currentScope);
        }

        /// <summary>
        /// Adds a transient instance of <paramref name="entity"/> to be tracked
        /// and persisted by the repository.
        /// </summary>
        /// <param name="entity">An instance of <typeparamref name="TEntity"/> that should be added.</param>
        public abstract void Add(TEntity entity);

        /// <summary>
        /// Marks the entity instance to be deleted from the store.
        /// </summary>
        /// <param name="entity">An instance of <paramref name="entity"/> that should be deleted.</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="entity">The entity instance, currently being tracked via the repository, to detach.</param>
        public abstract void Detach(TEntity entity);

        /// <summary>
        /// Attaches a detached entity, previously detached via the <see cref="IRepository{TEntity}.Detach"/> method.
        /// </summary>
        /// <param name="entity">The entity instance to attach back to the repository.</param>
        public abstract void Attach(TEntity entity);

        /// <summary>
        /// Refreshes a entity instance.
        /// </summary>
        /// <param name="entity">The entity to refresh.</param>
        public abstract void Refresh(TEntity entity);

        /// <summary>
        /// Queries the repository based on the provided specification and returns results that
        /// are only satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TEntity}"/> instance used to filter results
        /// that only satisfy the specification.</param>
        /// <returns>A <see cref="IEnumerable{TEntity}"/> that can be used to enumerate over the results
        /// of the query.</returns>
        public IEnumerable<TEntity> Query(ISpecification<TEntity> specification) {
            return RepositoryQuery.Where(specification.Predicate).AsQueryable();
        }

        /// <summary>
        /// Defines the service context under which the repository will execute.
        /// </summary>
        /// <typeparam name="TService">The service type that defines the context of the repository.</typeparam>
        /// <returns>The same <see cref="IRepository{TEntity}"/> instance.</returns>
        /// <remarks>
        /// Implementors should perform context specific actions within this method call and return
        /// the exact same instance.
        /// </remarks>
        public IQueryable<TEntity> For<TService>() {
            var strategy = ServiceLocator
                .Current
                .GetAllInstances<IFetchingStrategy<TEntity, TService>>()
                .FirstOrDefault();

            if (strategy != null)
                return strategy.Define(this);
            return this;
        }
    }
}