namespace TetriBricks.Core;

public class Brick
{
    public Brick()
    {
    }

    public Brick(int row, int column, BrickColor color)
    {
        Row = row;
        Column = column;
        Color = color;
    }

    public int Row { get; set; }
    public int Column { get; set; }
    public BrickColor Color { get; set; }
}
