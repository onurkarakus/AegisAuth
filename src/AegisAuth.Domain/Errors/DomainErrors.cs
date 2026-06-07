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

    public static class Auth
    {
        public static readonly Error TenantNotFound = Error.NotFound("AUTH_001", "Tenant Not Found", "The specified tenant was not found.");
        public static readonly Error UserAlreadyExists = Error.Conflict("AUTH_002", "User Already Exists", "A user with the specified email already exists.");
        public static readonly Error InvalidCredentials = Error.Unauthorized("AUTH_003", "Invalid Credentials", "The provided credentials are invalid.");
        public static readonly Error RefreshTokenExpired = Error.Unauthorized("AUTH_004", "Refresh Token Expired", "The refresh token has expired.");
        public static readonly Error RefreshTokenInvalid = Error.Unauthorized("AUTH_005", "Refresh Token Invalid", "The refresh token is invalid.");
        public static readonly Error UnsupportedGrantType = Error.Validation("AUTH_006", "Unsupported Grant Type", "The specified grant type is not supported.");
        public static readonly Error InvalidClient = Error.Unauthorized("AUTH_007", "Invalid Client", "The specified client is invalid.");
    }
}
