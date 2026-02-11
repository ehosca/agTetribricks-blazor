using Microsoft.EntityFrameworkCore;
using TetriBricks.Server.Data;
using TetriBricks.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tetribricks.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapGet("/api/scores", async (AppDbContext db) =>
{
    var scores = await db.Scores
        .OrderByDescending(s => s.ScoreValue)
        .Take(20)
        .Select(s => new ScoreDto
        {
            Id = s.Id,
            UserName = s.UserName,
            ScoreValue = s.ScoreValue,
            ScoreDate = s.ScoreDate,
            EmailAddress = s.EmailAddress
        })
        .ToListAsync();

    return Results.Ok(scores);
});

app.MapPost("/api/scores", async (SubmitScoreRequest request, AppDbContext db) =>
{
    var score = new TbScore
    {
        Id = Guid.NewGuid(),
        UserName = request.UserName,
        ScoreValue = request.ScoreValue,
        ScoreDate = DateTime.UtcNow,
        EmailAddress = request.EmailAddress
    };

    db.Scores.Add(score);
    await db.SaveChangesAsync();

    return Results.Created($"/api/scores/{score.Id}", new ScoreDto
    {
        Id = score.Id,
        UserName = score.UserName,
        ScoreValue = score.ScoreValue,
        ScoreDate = score.ScoreDate,
        EmailAddress = score.EmailAddress
    });
});

app.MapFallbackToFile("index.html");

app.Run();
