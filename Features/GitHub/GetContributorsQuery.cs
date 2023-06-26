using MediatR;

namespace GitHubAPI.Features.GitHub;

public class GetContributorsQuery : IRequest<List<string>>
{
    public string? Owner { get; init; }
    public string? Repo { get; init; }
}