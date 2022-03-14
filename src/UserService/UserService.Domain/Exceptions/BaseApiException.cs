using System.Net;

namespace UserService.Domain.Exceptions;

public class BaseApiException : Exception
{
    public int Code { get; }
    public string Description { get; }
    public BaseApiException() : base("Error")
    {
        Code = 500;
        Description = "Error";
    }
    public BaseApiException(string message, string description, int code) : base(message)
    {
        Code = code;
        Description = description;
    }
}

public class ApiException : BaseApiException
{
    public ApiException()
    {
    }
    public ApiException(HttpStatusCode statusCode, string description)
        : base($"{statusCode}: {description}", description, (int)statusCode)
    {
    }

    public ApiException(string statusCode, string description)
        : base($"{statusCode} {description}", description,
            int.TryParse(statusCode, out int parsed) ? parsed : 500)
    {
    }
}

public class CustomErrorResponse
{
    public string Message { get; set; }
    public string Description { get; set; }
}