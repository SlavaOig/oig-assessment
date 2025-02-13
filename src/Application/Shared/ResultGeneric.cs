﻿namespace oig_assessment.Application.Shared;

public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public string? Error { get; }

    private Result(bool success, T? value, string? error)
    {
        Success = success;
        Value = value;
        Error = error;
    }

    public static Result<T> SuccessResult(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}
