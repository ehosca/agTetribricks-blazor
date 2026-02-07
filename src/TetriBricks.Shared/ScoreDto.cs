namespace TetriBricks.Shared;

public class ScoreDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int ScoreValue { get; set; }
    public DateTime ScoreDate { get; set; }
    public string? EmailAddress { get; set; }
}
