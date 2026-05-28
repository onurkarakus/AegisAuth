using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AegisAuth.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public string ErrorMessage { get; set; }

    protected internal Result(bool isSuccess, string errorMessage)
    {
        if (isSuccess && !string.IsNullOrEmpty(errorMessage))
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && string.IsNullOrEmpty(errorMessage))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new Result(true, string.Empty);
    public static Result Failure(string errorMessage) => new Result(false, errorMessage);

    public static Result<T> Success<T>(T value) => new Result<T>(value, true, string.Empty);
    public static Result<T> Failure<T>(string errorMessage) => new Result<T>(default, false, errorMessage);



}

public class Result<T> : Result
{
    private readonly T? value;

    protected internal Result(T? value, bool isSuccess, string ErrorMessage) : base(isSuccess, ErrorMessage)
    {
        this.value = value;
    }

    public T Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("Cannot access the value of a failed result.");
            }

            return value!;
        }
    }

    public static implicit operator Result<T>(T value) => value is not null ? Success(value) : Failure<T>("Value cannot be null.");
    public static implicit operator Result<T>(string errorMessage) => Failure<T>(errorMessage);
}
