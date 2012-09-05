﻿namespace TechDemo.Data.EntityFramework {
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Inherits from the <see cref="RepositoryBase{TEntity}"/> class to provide an implementation of a
    /// Repository that uses Entity Framework.
    /// </summary>
    public class EFRepository<TEntity> : RepositoryBase<TEntity> where TEntity : class {
        readonly IEFSession _privateSession;
        readonly List<string> _includes = new List<string>();

        /// <summary>
        /// Creates a new instance of the <see cref="EFRepository{TEntity}"/> class.
        /// </summary>
        public EFRepository() {
            if (ServiceLocator.Current == null)
                return;

            var sessions = ServiceLocator.Current.GetAllInstances<IEFSession>();
            if (sessions != null && sessions.Count() > 0)
                _privateSession = sessions.First();
        }

        /// <summary>
        /// Gets the <see cref="ObjectContext"/> to be used by the repository.
        /// </summary>
        private IEFSession Session {
            get {
                if (_privateSession != null)
                    return _privateSession;
                var unitOfWork = UnitOfWork<EFUnitOfWork>();
                return unitOfWork.GetSession<TEntity>();

            }
        }

        /// <summary>
        /// Gets the <see cref="IQueryable{TEntity}"/> used by the <see cref="RepositoryBase{TEntity}"/> 
        /// to execute Linq queries.
        /// </summary>
        /// <value>A <see cref="IQueryable{TEntity}"/> instance.</value>
        protected override IQueryable<TEntity> RepositoryQuery {
            get {
                var query = Session.CreateQuery<TEntity>();
                if (_includes.Count > 0)
                    _includes.ForEach(x => query = query.Include(x));
                return query;
            }
        }

        /// <summary>
        /// Adds a transient instance of <typeparamref cref="TEntity"/> to be tracked
        /// and persisted by the repository.
        /// </summary>
        /// <param name="entity"></param>
        public override void Add(TEntity entity) {
            Session.Add(entity);
        }

        /// <summary>
        /// Marks the entity instance to be deleted from the store.
        /// </summary>
        /// <param name="entity">An instance of <typeparamref name="TEntity"/> that should be deleted.</param>
        public override void Delete(TEntity entity) {
            Session.Delete(entity);
        }

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="entity">The entity instance, currently being tracked via the repository, to detach.</param>
        /// <exception cref="NotImplementedException">Implentors should throw the NotImplementedException if Detaching
        /// entities is not supported.</exception>
        public override void Detach(TEntity entity) {
            Session.Detach(entity);
        }

        /// <summary>
        /// Attaches a detached entity, previously detached via the <see cref="IRepository{TEntity}.Detach"/> method.
        /// </summary>
        /// <param name="entity">The entity instance to attach back to the repository.</param>
        /// <exception cref="NotImplementedException">Implentors should throw the NotImplementedException if Attaching
        /// entities is not supported.</exception>
        public override void Attach(TEntity entity) {
            Session.Attach(entity);
        }

        /// <summary>
        /// Refreshes a entity instance.
        /// </summary>
        /// <param name="entity">The entity to refresh.</param>
        /// <exception cref="NotImplementedException">Implementors should throw the NotImplementedException if Refreshing
        /// entities is not supported.</exception>
        public override void Refresh(TEntity entity) {
            Session.Refresh(entity);
        }

        internal void AddInclude(string includePath) {
            _includes.Add(includePath);
        }
    }
}