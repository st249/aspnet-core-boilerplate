using System.Net;

namespace CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions;

public class InvalidInputException : BaseException
{
    private const string _defaultMessage = "Your input is not valid.";
    public InvalidInputException() : base(_defaultMessage)
    {

    }
    public InvalidInputException(string message) : base(message)
    {

    }
    public InvalidInputException(string message, HttpStatusCode statusCode) : this($"{message} Status Code:{statusCode}")
    {

    }
    public InvalidInputException(HttpStatusCode statusCode) : this(_defaultMessage, statusCode)
    {

    }
}


