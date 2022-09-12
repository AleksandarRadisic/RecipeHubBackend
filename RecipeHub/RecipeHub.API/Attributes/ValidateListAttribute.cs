using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace RecipeHub.API.Attributes
{
    public class ValidateListAttribute : ValidationAttribute
    {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        private readonly bool _notEmptyRequired;

        public ValidateListAttribute(bool notEmptyRequired)
        {
            _notEmptyRequired = notEmptyRequired;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IEnumerable;
            if (_notEmptyRequired)
            {
                bool isEmpty = true;
                foreach (var item in list) isEmpty = false;
                if (isEmpty) return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

    }
}
