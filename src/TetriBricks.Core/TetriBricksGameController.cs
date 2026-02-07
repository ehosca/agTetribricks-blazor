namespace TetriBricks.Core;

public class TetriBricksGameController
{
    private static readonly Random _rnd = new Random();
    private readonly List<TetriBricksGame> _moveHistory = new List<TetriBricksGame>();
    private int _currentMove;

    public TetriBricksGameController(int rows, int columns)
    {
        CreateNewGame(rows, columns);
    }

    public TetriBricksGameController()
    {
        CreateNewGame();
    }

    public int TotalScore => CurrentGame.Score;

    public bool CanUndo => _currentMove > 0;

    public bool CanRedo => _currentMove < _moveHistory.Count - 1;

    public TetriBricksGame CurrentGame
    {
        get { return _moveHistory[_currentMove]; }
    }

    public void CreateNewGame()
    {
        CreateNewGame(15, 15);
    }

    public void CreateNewGame(int rows, int columns)
    {
        _moveHistory.Clear();

        var game = new TetriBricksGame();

        for (int i = 0; i < columns; i++)
        {
            var bc = new BrickColumn();
            for (int j = 0; j < rows; j++)
            {
                bc.Bricks.Add(new Brick(j, i, GetRandomColor()));
            }
            game.Columns.Add(bc);
        }
        _moveHistory.Add(game.Clone());
        _currentMove = 0;
    }

    private BrickColor GetRandomColor()
    {
        return (BrickColor)_rnd.Next(4);
    }

    public bool RemoveBrick(Brick brick)
    {
        if (_moveHistory.Count - 1 > _currentMove)
        {
            _moveHistory.RemoveRange(_currentMove + 1, (_moveHistory.Count - 1 - _currentMove));
            _currentMove = _moveHistory.Count - 1;
        }

        List<Brick> bricks = CurrentGame.GetAdjacentBricks(brick);

        if (bricks.Count > 1)
        {
            var gameStep = CurrentGame.Clone();

            gameStep.RemoveBricks(bricks);
            gameStep.Score = gameStep.Score + CurrentGame.Score;

            _moveHistory.Add(gameStep);
            _currentMove = _moveHistory.Count - 1;

            return true;
        }
        else
            return false;
    }

    public TetriBricksGame Previous()
    {
        _currentMove--;

        if (_currentMove < 0)
            _currentMove = 0;

        return CurrentGame;
    }

    public TetriBricksGame Next()
    {
        _currentMove++;
        if (_currentMove > _moveHistory.Count - 1)
            _currentMove = _moveHistory.Count - 1;

        return CurrentGame;
    }
}
