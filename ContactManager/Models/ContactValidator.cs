using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ContactManager.Models
{
    public class CustomEmailValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                Person contact = (Person)validationContext.ObjectInstance;
                if (contact.Category != Person.Customer)
                {
                    return ValidationResult.Success;
                }

                RequiredAttribute required = new RequiredAttribute();
                if (!required.IsValid(value))
                {
                    return new ValidationResult($"Email is required for a {Person.Customer}.");
                }

                EmailAddressAttribute emailAddress = new EmailAddressAttribute();
                if (!emailAddress.IsValid(value))
                {
                    return new ValidationResult($"Email format is incorrect.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("Somrthing wrong while validating Email!" + ex.Message);
            }

            return ValidationResult.Success;
        }
    }
    public class CustomBirthdayValidatorAttribute : ValidationAttribute
    {
        public string Pattern { get; set; } = @"^(19|20)\d{2}\-((0[1-9])|(1[0-2]))\-((0[1-9])|([12][0-9])|(3[01]))$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                Person contact = (Person)validationContext.ObjectInstance;
                if (contact.Category != Person.Customer || string.IsNullOrEmpty(value.ToString()))
                {
                    return ValidationResult.Success;
                }

                RegularExpressionAttribute regex = new RegularExpressionAttribute(Pattern);
                if (!regex.IsValid(value))
                {
                    return new ValidationResult($"Birthday should be a valid date, format: yyyy-mm-dd");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("Somrthing wrong while validating Birthday!" + ex.Message);
            }

            return ValidationResult.Success;
        }
    }

    public class CustomTelephoneValidatorAttribute : ValidationAttribute
    {
        public string Pattern { get; set; } = @"^\d{7,12}$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                Person contact = (Person)validationContext.ObjectInstance;
                if (contact.Category != Person.Supplier)
                {
                    return ValidationResult.Success;
                }

                RequiredAttribute required = new RequiredAttribute();
                if (!required.IsValid(value))
                {
                    return new ValidationResult($"Telephone is required for a {Person.Supplier}.");
                }

                RegularExpressionAttribute regex = new RegularExpressionAttribute(Pattern);
                if (!regex.IsValid(value))
                {
                    return new ValidationResult($"Telephone number should be at least 7 and no more than 12 digits.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("Somrthing wrong while validating Telephone Number!" + ex.Message);
            }

            return ValidationResult.Success;
        }
    }

    public class CustomCategoryValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (!value.ToString().Equals(Person.Customer) && !value.ToString().Equals(Person.Supplier))
                {
                    return new ValidationResult($"Contact Category should be either [{Person.Customer}] or [{Person.Supplier}].");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("Somrthing wrong while validating Category!" + ex.Message);
            }

            return ValidationResult.Success;
        }
    }
}
