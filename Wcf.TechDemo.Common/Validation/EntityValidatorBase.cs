namespace TechDemo.Validation {
    using System.Collections.Generic;

    ///<summary>
    /// Base class that implementors of <see cref="IEntityValidator{TEntity}"/> can use to 
    /// provide validation logic for their entities.
    ///</summary>
    ///<typeparam name="TEntity">The type of entity that validation will be performed against.</typeparam>
    public abstract class EntityValidatorBase<TEntity> : IEntityValidator<TEntity> where TEntity : class {
        //The internal dictionary used to store rule sets.
        private readonly Dictionary<string, IValidationRule<TEntity>> _validations = new Dictionary<string, IValidationRule<TEntity>>();

        /// <summary>
        /// Adds a <see cref="IValidationRule{TEntity}"/> instance to the entity validator.
        /// </summary>
        /// <param name="rule">The <see cref="IValidationRule{TEntity}"/> instance to add.</param>
        /// <param name="ruleName">string. The unique name assigned to the validation rule.</param>
        protected virtual void AddValidation(string ruleName, IValidationRule<TEntity> rule) {
            _validations.Add(ruleName, rule);
        }

        /// <summary>
        /// Removes a previously added rule, specified with the <paramref name="ruleName"/>, from the evaluator.
        /// </summary>
        /// <param name="ruleName">string. The name of the rule to remove.</param>
        protected virtual void RemoveValidation(string ruleName) {
            _validations.Remove(ruleName);
        }

        /// <summary>
        /// Validates an entity against all validations defined for the entity.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to validate.</param>
        /// <returns>A <see cref="ValidationResult"/> that contains the results of the validation.</returns>
        public virtual ValidationResult Validate(TEntity entity) {
            var result = new ValidationResult();
            _validations.Keys.ForEach(x => {
                                              var rule = _validations[x];
                                              if (!rule.Validate(entity))
                                                  result.AddError(new ValidationError(rule.ValidationMessage,
                                                                                        rule.ValidationProperty));
                                          });
            return result;
        }

        /// <summary>
        /// Gets a <see cref="IValidationRule{TEntity}"/> that was added to the validator with the specified
        /// rule name.
        /// </summary>
        /// <param name="ruleName">The name of the validation rule to retrieve.</param>
        /// <returns>A <see cref="IValidationRule{TEntity}"/> instance, or null if no rule stored with the specified
        /// rule name was found.</returns>
        protected IValidationRule<TEntity> GetValidationRule(string ruleName) {
            IValidationRule<TEntity> rule;
            _validations.TryGetValue(ruleName, out rule);
            return rule;
        }
    }

}
