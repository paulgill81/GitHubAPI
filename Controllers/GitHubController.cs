using GitHubAPI.Features.GitHub;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHubAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1")]
public class GitHubController : ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{owner}/{repo}/contributors")]
    public async Task<ActionResult<List<string>>> GetContributors(string owner, string repo)
    {
        var contributors = await _mediator.Send(new GetContributorsQuery {Owner = owner, Repo = repo});

        return Ok(contributors);
    }
}