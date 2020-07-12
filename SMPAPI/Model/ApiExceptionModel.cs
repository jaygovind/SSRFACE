using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Model
{
    public class ApiExceptionModel : Exception
    {
        public int StatusCode { get; set; }
        public ApiExceptionModel(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }

    public class ApiError
    {
        public string message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }
        public ApiError(string message)
        {
            this.message = message;
            isError = true;
        }
        public ApiError(ModelStateDictionary modelState)
        {
            this.isError = true;
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                message = "Please correct the specified errors and try again.";
            }
        }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Stacktrace { get; set; }
    }
}
