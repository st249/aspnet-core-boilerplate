namespace CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}


