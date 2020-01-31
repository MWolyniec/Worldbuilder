using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace Worldbuilder.Helpers
{
    public static class ModelChecker
    {
        public static void Check(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var err = modelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .ToList();
                throw new Exception($"Model is invalid, ModelState has {err.Count} errors. Try turning the page off and on again.");
            }
        }
    }
}
