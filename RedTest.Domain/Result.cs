namespace RedTest.Domain;

public class Result<T> : Result
{
    public T? Data { get; set; }
}

public class Result
{
    public string? ErrorMessage { get; set; }
}

public static class ResultFactory
{
    public static Result<T> Success<T>(T data)
    {
        return new Result<T> { Data = data };
    }

    public static Result<T> Error<T>(string errorMessage)
    {
        return new Result<T> { ErrorMessage = errorMessage };
    }

}