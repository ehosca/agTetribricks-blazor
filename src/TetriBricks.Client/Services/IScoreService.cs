using TetriBricks.Shared;

namespace TetriBricks.Client.Services;

public interface IScoreService
{
    Task<List<ScoreDto>> GetTopScoresAsync();
    Task SaveScoreAsync(SubmitScoreRequest request);
}
