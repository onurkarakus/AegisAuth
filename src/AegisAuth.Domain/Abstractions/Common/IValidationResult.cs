using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AegisAuth.Domain.Errors;
using AegisAuth.Domain.Shared;

namespace AegisAuth.Domain.Abstractions.Common;

public interface IValidationResult
{
    public static readonly Error ValidationError = DomainErrors.ValidationErrors.ValidationFailed;

    Error[] Errors { get; }
}
