using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TennisTournament.Validator
{
    public class NotCompareAttribute : ValidationAttribute
    {
        public NotCompareAttribute(string otherProperty) 
        {
            OtherProperty = otherProperty ;
        }

        public string OtherProperty { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);

            object? otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (Equals(value, otherPropertyValue))
            {
                return new ValidationResult("Le même joueur a été sélectionné");
            }

            return ValidationResult.Success;
        }

    }
}
