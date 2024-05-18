using System.Net;

namespace Syndicate.Services;

public class ApiResponse
{
    public ApiError? Error { get; set; }

    public static ApiResponse Happy()
    {
        return new();
    }

    public static ApiResponse Fail(HttpStatusCode errorCode, string message = "Something went wrong")
    {
        return new ApiResponse { Error = new() { Message = message, Code = ((int)errorCode).ToString() } };
    }
}

public class ApiResponse<TData>
    where TData : class
{
    public TData? Data { get; set; }

    public ApiError<TData>? Error { get; set; }

    public ApiResponse(HttpStatusCode errorCode, string message)
    {
        Error = new()
        {
            Message = message,
            Code = ((int)errorCode).ToString()
        };
    }

    public ApiResponse(HttpStatusCode errorCode, string message, TData data)
    {
        Data = data;
        Error = new()
        {
            Message = message,
            Code = ((int)errorCode).ToString()
        };
    }

    public ApiResponse(TData data) => Data = data;

    public ApiResponse() { }
}

public class ApiError<TData>
    where TData : class
{
    public required string Message { get; set; }

    public required string Code { get; set; }

    public TData? Data { get; set; }
}


public class ApiError
{
    public required string Message { get; set; }

    public required string Code { get; set; }
}