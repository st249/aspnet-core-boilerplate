namespace CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions;

public class BusResponseException : BaseException
{
    private const string _defaultMessage = "can not handle the request.";
    public BusResponseException() : base(_defaultMessage)
    {

    }
    public BusResponseException(string message) : base(message)
    {

    }
}


