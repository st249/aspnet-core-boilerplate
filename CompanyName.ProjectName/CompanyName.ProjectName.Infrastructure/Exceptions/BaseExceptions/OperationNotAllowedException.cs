namespace CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions;

public class OperationNotAllowedException : BaseException
{
    public OperationNotAllowedException(string message) : base(message)
    {
    }

    public OperationNotAllowedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

