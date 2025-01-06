namespace CompanyName.ProjectName.Domain.Exceptions;

public class ProjectNameDomainException : Exception
{
    public ProjectNameDomainException()
    { }

    public ProjectNameDomainException(string message)
        : base(message)
    { }

    public ProjectNameDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}



