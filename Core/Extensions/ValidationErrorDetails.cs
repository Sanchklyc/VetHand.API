using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public List<ValidationFailure> Errors { get; set; }
    }
}
