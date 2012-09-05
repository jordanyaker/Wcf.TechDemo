namespace TechDemo.Data.EntityFramework {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TechDemo.Expressions;

    public static class EFRepositoryExtensions {
        public static IEFFetchingRepository<TEntity, TReleated> Fetch<TEntity, TReleated>(this IRepository<TEntity> repository, Expression<Func<TEntity, TReleated>> selector) where TEntity : class {
            var efRepository = repository as EFRepository<TEntity>;

            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(selector);
            efRepository.AddInclude(visitor.Path);

            return (IEFFetchingRepository<TEntity, TReleated>)
                Activator.CreateInstance(typeof(EFFetchingRepository<TEntity, TReleated>), efRepository, visitor.Path);
        }

        public static IEFFetchingRepository<TEntity, TReleated> FetchMany<TEntity, TReleated>(this IRepository<TEntity> repository, Expression<Func<TEntity, IEnumerable<TReleated>>> selector) where TEntity : class {
            var efRepository = repository as EFRepository<TEntity>;

            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(selector);
            efRepository.AddInclude(visitor.Path);

            return (IEFFetchingRepository<TEntity, TReleated>)
                Activator.CreateInstance(typeof(EFFetchingRepository<TEntity, TReleated>), efRepository, visitor.Path);
        }

        public static IEFFetchingRepository<TEntity, TReleated> ThenFetch<TEntity, TFetch, TReleated>(this IEFFetchingRepository<TEntity, TFetch> repository, Expression<Func<TFetch, TReleated>> selector) where TEntity : class {
            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(selector);
            var includePath = repository.FetchingPath + "." + visitor.Path;
            repository.RootRepository.AddInclude(includePath);

            return (IEFFetchingRepository<TEntity, TReleated>)
                Activator.CreateInstance(typeof(EFFetchingRepository<TEntity, TReleated>), repository.RootRepository, includePath);
        }

        public static IEFFetchingRepository<TEntity, TReleated> ThenFetchMany<TEntity, TFetch, TReleated>(this IEFFetchingRepository<TEntity, TFetch> repository, Expression<Func<TFetch, IEnumerable<TReleated>>> selector) where TEntity : class {
            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(selector);
            var includePath = repository.FetchingPath + "." + visitor.Path;
            repository.RootRepository.AddInclude(includePath);

            return (IEFFetchingRepository<TEntity, TReleated>)
                Activator.CreateInstance(typeof(EFFetchingRepository<TEntity, TReleated>), repository.RootRepository, includePath);
        }
    }
}