namespace CompanyName.ProjectName.Contracts.ServiceBus
{
    public class BusResponse
    {
        public virtual Error Error { get; set; }

        public virtual bool HasError()
        {
            return Error != null;
        }
    }
}


