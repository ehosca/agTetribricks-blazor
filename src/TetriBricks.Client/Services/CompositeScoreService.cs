using TetriBricks.Shared;

namespace TetriBricks.Client.Services;

public class CompositeScoreService : IScoreService
{
    private readonly ApiScoreService _apiService;
    private readonly LocalScoreService _localService;

    public CompositeScoreService(ApiScoreService apiService, LocalScoreService localService)
    {
        _apiService = apiService;
        _localService = localService;
    }

    public async Task<List<ScoreDto>> GetTopScoresAsync()
    {
        try
        {
            return await _apiService.GetTopScoresAsync();
        }
        catch
        {
            return await _localService.GetTopScoresAsync();
        }
    }

    public async Task SaveScoreAsync(SubmitScoreRequest request)
    {
        await _localService.SaveScoreAsync(request);

        try
        {
            await _apiService.SaveScoreAsync(request);
        }
        catch
        {
            // API unavailable — local save is sufficient
        }
    }
}
