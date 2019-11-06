using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace SignalRProject.BusinessLogic.Common.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {
            string result = modelState
                .Values
                .SelectMany(x => x.Errors)
                .Select(d=>d.ErrorMessage)
                .FirstOrDefault();

            return result;
        }
    }
}
