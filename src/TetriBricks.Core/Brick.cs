namespace TetriBricks.Core;

public class Brick
{
    private static int _nextId = 1;

    public Brick()
    {
    }

    public Brick(int row, int column, BrickColor color)
    {
        Id = _nextId++;
        Row = row;
        Column = column;
        Color = color;
    }

    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public BrickColor Color { get; set; }
}
