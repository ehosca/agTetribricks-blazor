namespace TetriBricks.Shared;

public class SubmitScoreRequest
{
    public string UserName { get; set; } = string.Empty;
    public int ScoreValue { get; set; }
    public string? EmailAddress { get; set; }
}
