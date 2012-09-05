namespace TechDemo.Data {
    using System.Linq;

    ///<summary>
    /// Specifies a fetching strategy for a <see cref="IRepository{TEntity}"/> instance.
    ///</summary>
    public interface IFetchingStrategy<TEntity, TForService> {
        ///<summary>
        /// Instructs the instance to define the fetching strategy on the repository instance.
        ///</summary>
        ///<param name="repository"></param>
        IQueryable<TEntity> Define(IRepository<TEntity> repository);
    }
}