using Blazored.LocalStorage;
using TetriBricks.Shared;

namespace TetriBricks.Client.Services;

public class LocalScoreService : IScoreService
{
    private const string StorageKey = "tetribricks_scores";
    private readonly ILocalStorageService _localStorage;

    public LocalScoreService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<List<ScoreDto>> GetTopScoresAsync()
    {
        var scores = await _localStorage.GetItemAsync<List<ScoreDto>>(StorageKey);
        if (scores == null)
            return new List<ScoreDto>();

        return scores.OrderByDescending(s => s.ScoreValue).Take(20).ToList();
    }

    public async Task SaveScoreAsync(SubmitScoreRequest request)
    {
        var scores = await _localStorage.GetItemAsync<List<ScoreDto>>(StorageKey)
                     ?? new List<ScoreDto>();

        scores.Add(new ScoreDto
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            ScoreValue = request.ScoreValue,
            ScoreDate = DateTime.UtcNow,
            EmailAddress = request.EmailAddress
        });

        scores = scores.OrderByDescending(s => s.ScoreValue).Take(20).ToList();
        await _localStorage.SetItemAsync(StorageKey, scores);
    }
}
