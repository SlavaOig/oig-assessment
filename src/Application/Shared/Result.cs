namespace oig_assessment.Application.Shared;
public class Result
{
    public bool Success { get; }
    public string? Error { get; }

    protected Result(bool success, string? error)
    {
        Success = success;
        Error = error;
    }

    public static Result SuccessResult() => new Result(true, null);
    public static Result Failure(string error) => new Result(false, error);
}
