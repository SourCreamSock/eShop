using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Web.Application.Validatiors;
using FluentValidation.Results;

namespace Web.Application.Utils.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddToApiModelState(this ValidationResult result, System.Web.Http.ModelBinding.ModelStateDictionary modelState )
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
        public static void AddToMVCModelState(this ValidationResult result, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }

    }
}
