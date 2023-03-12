using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EntertenimentManagement.API.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors (this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var item in modelState.Values)
                result.AddRange(item.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}
