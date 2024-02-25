namespace RedTest.Shared;

public class Result<T> : Result
{
    public T? Data { get; set; }
}

public class Result
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}

public static class ResultFactory
{

    public static Result Success()
    {
        return new Result { IsSuccess = true };
    }
    
    public static Result Error(string errorMessage)
    {
        return new Result { IsSuccess = false, ErrorMessage = errorMessage };
    }

    public static Result<T> Success<T>(T data)
    {
        return new Result<T> { Data = data, IsSuccess = true };
    }

    public static Result<T> Error<T>(string errorMessage)
    {
        return new Result<T> { ErrorMessage = errorMessage, IsSuccess = false };
    }

}