using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class CommonApiController : ControllerBase
{
    private IConfiguration _configuration;
    private IMediator _mediator;
    
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    protected IConfiguration Configuration => _configuration??= HttpContext.RequestServices.GetService<IConfiguration>()!;
    
    protected string UrlResponse => Configuration.GetSection("PageUrl")["Https"] ?? throw new InvalidOperationException("Check pageUrl in appsettings.json");
};