using System.ComponentModel.DataAnnotations;

namespace Galicia.Test.Shared.CustomValidations
{
    public class MinValuePropertyValidation : ValidationAttribute
    {
        public int minValue { get; }
        public MinValuePropertyValidation(int minValue)
        {
            this.minValue = minValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((int)value <= minValue)
                return new ValidationResult(base.ErrorMessage);
            
            return ValidationResult.Success;
        }

    }
}
