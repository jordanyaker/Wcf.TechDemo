namespace TechDemo.Validation {
    using TechDemo.Specifications;

    /// <summary>
    /// Base implementation that uses <see cref="ISpecification{TEntity}"/> instances that provide the logic to check if the
    /// rule is satisfied.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that validation will be performed against.</typeparam>
    public abstract class SpecificationRuleBase<TEntity> {
        private readonly ISpecification<TEntity> _rule; //The underlying rule as a specification.

        /// <summary>
        /// Default Constructor. 
        /// Protected. Must be called by implementors.
        /// </summary>
        /// <param name="rule">A <see cref="ISpecification{TEntity}"/> instance that specifies the rule.</param>
        protected SpecificationRuleBase(ISpecification<TEntity> rule) {
            _rule = rule;
        }

        /// <summary>
        /// Checks if the entity instance satisfies this rule.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> insance.</param>
        /// <returns>bool. True if the rule is satsified, else false.</returns>
        public bool IsSatisfied(TEntity entity) {
            return _rule.IsSatisfiedBy(entity);
        }
    }
}
