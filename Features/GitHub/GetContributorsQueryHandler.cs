using MediatR;
using Newtonsoft.Json;

namespace GitHubAPI.Features.GitHub;

public class GetContributorsQueryHandler : IRequestHandler<GetContributorsQuery, List<string>>
{
    private readonly HttpClient _httpClient;

    public GetContributorsQueryHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> Handle(GetContributorsQuery request, CancellationToken cancellationToken)
    {
        var url = $"https://api.github.com/repos/{request.Owner}/{request.Repo}/commits?per_page=30";

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "request");

        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var gitCommits = JsonConvert.DeserializeObject<List<GitCommit>>(json);

        return gitCommits == null
            ? new List<string>()
            : gitCommits.Select(gitCommit => gitCommit.Commit.Author.Name).Distinct().ToList();
    }
}

public class GitCommit
{
    public Commit Commit { get; set; }
}

public class Commit
{
    public Author Author { get; set; }
}

public class Author
{
    public string Name { get; set; }
}