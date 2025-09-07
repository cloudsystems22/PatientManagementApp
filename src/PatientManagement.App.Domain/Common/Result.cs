namespace PatientManagement.App.Domain.Common;

/// <summary>
/// Represents the outcome of an operation, containing either a successful result or an error.
/// </summary>
/// <typeparam name="T">The type of the data returned on success.</typeparam>
public class Result<T>
{
    public bool Success { get; private set; }
    public string? Error { get; private set; }
    public T? Data { get; private set; }

    // Private constructor to enforce creation via static methods.
    private Result(bool success, T? data, string? error)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static Result<T> Ok(T data) => new(true, data, null);
    public static Result<T> Fail(string error) => new(false, default, error);
}

