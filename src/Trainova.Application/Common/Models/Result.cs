namespace Trainova.Application.Common.Models;

public class Result<T>
{
    private Result(bool success, T? data, string message, string[] errors)
    {
        Success = success;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public bool Success { get; }
    public string Message { get; }
    public T? Data { get; }
    public string[] Errors { get; }

    // For operations that succeeds and returns data
    public static Result<T> Ok(T data, string message = "Success") => new(true, data, message, []);

    // For operations that succeeds but returns no data
    public static Result<T> Ok(string message = "Success") => new(true, default, message, []);

    // For validation failures
    public static Result<T> Fail(string message, params string[] errors) => new(false, default, message, errors);

    // For exceptions
    public static Result<T> Error(string message = "Something went wrong") => new(false, default, message, []);
}
