using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Common.ResponseObjects;
using Udemy.ToDoAppNTier.Dtos.Interfaces;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;

namespace Udemy.ToDoAppNTier.Business.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationError> errors = new();
            foreach (var error in validationResult.Errors)
            {
                errors.Add(new()
                {
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });
            }
            return errors;
        }
    }
}
