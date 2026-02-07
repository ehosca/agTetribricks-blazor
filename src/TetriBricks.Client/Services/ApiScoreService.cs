using System.Net.Http.Json;
using TetriBricks.Shared;

namespace TetriBricks.Client.Services;

public class ApiScoreService : IScoreService
{
    private readonly HttpClient _http;

    public ApiScoreService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ScoreDto>> GetTopScoresAsync()
    {
        var scores = await _http.GetFromJsonAsync<List<ScoreDto>>("api/scores");
        return scores ?? new List<ScoreDto>();
    }

    public async Task SaveScoreAsync(SubmitScoreRequest request)
    {
        await _http.PostAsJsonAsync("api/scores", request);
    }
}
