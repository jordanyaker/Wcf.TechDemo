namespace TechDemo.Validation {
    using TechDemo.Specifications;

    /// <summary>
    /// Implements the <see cref="IValidationRule{TEntity}"/> interface and inherits from the
    /// <see cref="SpecificationRuleBase{TEntity}"/> to provide a very basic implementation of an
    /// entity validation rule that uses specifications as underlying rule logic.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ValidationRule<TEntity> : SpecificationRuleBase<TEntity>, IValidationRule<TEntity> {
        private readonly string _message;
        private readonly string _property;

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="ValidationRule{TEntity}"/> class.
        /// </summary>
        /// <param name="message">string. The validation message associated with the rule.</param>
        /// <param name="property">string. The generic or specific name of the property that was validated.</param>
        /// <param name="rule"></param>
        public ValidationRule(ISpecification<TEntity> rule, string message, string property)
            : base(rule) {
            _message = message;
            _property = property;
        }

        /// <summary>
        /// Gets the message of the validation rule.
        /// </summary>
        public string ValidationMessage {
            get { return _message; }
        }

        /// <summary>
        /// Gets a generic or specific name of a property that was validated.
        /// </summary>
        public string ValidationProperty {
            get { return _property; }
        }

        /// <summary>
        /// Validates whether the entity violates the validation rule or not.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> entity instance to validate.</param>
        /// <returns>Should return true if the entity instance is valid, else false.</returns>
        public bool Validate(TEntity entity) {
            return IsSatisfied(entity);
        }
    }
}
