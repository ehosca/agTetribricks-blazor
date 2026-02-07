namespace TetriBricks.Core;

public class BrickColumn
{
    public List<Brick> Bricks { get; set; }

    public BrickColumn()
    {
        Bricks = new List<Brick>();
    }
}
