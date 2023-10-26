using Microsoft.AspNetCore.Mvc;

namespace BookPublisher.WebUI.Controllers;

[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class BookPublisherController : ControllerBase
{
    
}
