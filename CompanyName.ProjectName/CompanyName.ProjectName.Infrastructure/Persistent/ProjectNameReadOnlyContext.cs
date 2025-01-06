namespace CompanyName.ProjectName.Infrastructure.Persistent;

public partial class ProjectNameContext
{
    public class ProjectNameReadOnlyContext : ProjectNameContext
    {
        public ProjectNameReadOnlyContext(DbContextOptions<ProjectNameContext> options) : base(options)
        {
        }
    }

}


