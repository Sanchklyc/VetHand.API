using Core.Utilities.Helpers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                 await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            if (e.GetType() == typeof(ValidationException))
            {
                List<ValidationFailure> errors = ((ValidationException)e).Errors.ToList();
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString()); ;
            }


            if(e.GetType() == typeof(AuthorizeException))
            {
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = 401,
                    Message = "Unauthorized",
                }.ToString()); ;
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = e.Message,
                Exception = e
            }.ToString());
        }
    }
}
