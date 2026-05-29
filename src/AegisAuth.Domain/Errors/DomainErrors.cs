using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AegisAuth.Domain.Shared;

namespace AegisAuth.Domain.Errors;

public static class DomainErrors
{
    public static class ValidationErrors
    {
        public static readonly Error ValidationFailed = Error.Validation("VAL_001", "Validation Failed", "One or more validation errors occurred.");
        public static readonly Error RequiredFieldMissing = Error.Validation("VAL_002", "Required Field Missing", "The field '{field}' is required.");
        public static readonly Error InvalidFormat = Error.Validation("VAL_003", "Invalid Format", "The field '{field}' has an invalid format.");
        public static readonly Error MaxLengthExceeded = Error.Validation("VAL_004", "Max Length Exceeded", "The field '{field}' exceeds maximum length of {max}.");
        public static readonly Error MinLengthRequired = Error.Validation("VAL_005", "Min Length Required", "The field '{field}' must be at least {min} characters.");
    }


}
