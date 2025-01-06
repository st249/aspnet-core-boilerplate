using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Application.Commands.Sample;

public class SampleCommandHandler : IRequestHandler<SampleCommand,Unit>
{
    public async Task<Unit> Handle(SampleCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(500);
        return Unit.Value;
    }
}


