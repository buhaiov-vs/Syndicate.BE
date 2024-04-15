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

    public static ApiResponse<TData> Happy()
    {
        return new ApiResponse<TData> { };
    }

    public static ApiResponse<TData> Happy(TData data)
    {
        return new ApiResponse<TData> { Data = data };
    }

    public static ApiResponse<TData> Fail(HttpStatusCode errorCode, string message, TData data)
    {
        return new ApiResponse<TData> { Error = new() { Message = message, Code = ((int)errorCode).ToString() }, Data = data };
    }

    public static ApiResponse<TData> Fail(HttpStatusCode errorCode, string message = "Something went wrong")
    {
        return new ApiResponse<TData> { Error = new() { Message = message, Code = ((int)errorCode).ToString() } };
    }
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