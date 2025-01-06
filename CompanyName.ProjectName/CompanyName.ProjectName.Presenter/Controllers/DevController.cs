using CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.ProjectName.Presenter.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DevController : ControllerBase
{
    [HttpGet(Name = "TEST")]
    public IActionResult Test()
    {
        throw new DuplicateException("Something is duplicated");
    }

}


