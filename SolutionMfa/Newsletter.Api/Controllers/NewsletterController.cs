using Microsoft.AspNetCore.Mvc;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Api.Controllers;

[ApiController]
[Route("/api/newsletter")]
public class NewsletterController(INewsletterService newsletterService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        await newsletterService.SendAsync(cancellationToken);
        return Ok();
    }
}