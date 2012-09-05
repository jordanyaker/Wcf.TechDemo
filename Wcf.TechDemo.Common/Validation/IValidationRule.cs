namespace TechDemo.Validation {
    /// <summary>
    /// Provides a contract that defines a validation rule that provides validation logic  for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this validation rule is applicable for.</typeparam>
    public interface IValidationRule<TEntity> {
        /// <summary>
        /// Gets the message of the validation rule.
        /// </summary>
        string ValidationMessage { get; }

        /// <summary>
        /// Gets a generic or specific name of a property that was validated.
        /// </summary>
        string ValidationProperty { get; }

        /// <summary>
        /// Validates whether the entity violates the validation rule or not.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> entity instance to validate.</param>
        /// <returns>Should return true if the entity instance is valid, else false.</returns>
        bool Validate(TEntity entity);
    }
}
