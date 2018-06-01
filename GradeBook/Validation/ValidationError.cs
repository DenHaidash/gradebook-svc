using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GradeBook.Validation
{
    public class ValidationError
    {
        public string Error { get; } = "Некоректні дані";
        public Dictionary<string, List<string>> Data { get; }
        
        public ValidationError(ModelStateDictionary modelState, string errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Error = errorMessage;
            }
            
            Data = modelState.Select(err => new
            {
                Field = err.Key,
                Errors = err.Value.Errors.Select(e => e.ErrorMessage).ToList()
            }).ToDictionary(v => v.Field, v => v.Errors);
        }
    }
}