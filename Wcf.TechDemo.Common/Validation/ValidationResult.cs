namespace TechDemo.Validation {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the result of a <see cref="IEntityValidator{TEntity}.Validate"/> method call.
    /// </summary>
    [Serializable]
    public class ValidationResult {
        private readonly List<ValidationError> _errors = new List<ValidationError>();

        /// <summary>
        /// Gets wheater the validation operation on an entity was valid or not.
        /// </summary>
        public bool IsValid { get { return _errors.Count == 0; } }

        /// <summary>
        /// Gets an <see cref="IEnumerable{ValidationError}"/> that can be used to enumerate over
        /// the validation errors as a result of a <see cref="IEntityValidator{TEntity}.Validate"/> method
        /// call.
        /// </summary>
        public IEnumerable<ValidationError> Errors {
            get {
                foreach (var error in _errors)
                    yield return error;
            }
        }

        /// <summary>
        /// Adds a validation error into the result.
        /// </summary>
        /// <param name="error"></param>
        public void AddError(ValidationError error) {
            _errors.Add(error);
        }

        /// <summary>
        /// Removes a validation error from the result.
        /// </summary>
        /// <param name="error"></param>
        public void RemoveError(ValidationError error) {
            if (_errors.Contains(error))
                _errors.Remove(error);
        }
    }
}
