using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GradeBook.Validation
{
    public class ValidationError
    {
        public string Error { get; } = "Model is invalid";
        public Dictionary<string, List<string>> Data { get; }
        
        public ValidationError(ModelStateDictionary modelState)
        {
            Data = modelState.Select(err => new
            {
                Field = err.Key,
                Errors = err.Value.Errors.Select(e => e.ErrorMessage).ToList()
            }).ToDictionary(v => v.Field, v => v.Errors);
        }
    }
}