using CompanyName.ProjectName.Application.Behaviors;

namespace CompanyName.ProjectName.Application.Commands.Sample;

public record SampleCommand : ICommand<Unit>
{
}

public class SampleCommandValidator : AbstractValidator<SampleCommand>
{
    public SampleCommandValidator()
    {
        
    }
}


