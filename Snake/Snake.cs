public class Snake(int x, int y)
{
    public Direction Direction { get; set; }

    public bool Move()
    {
        switch (Direction)
        {
            case Direction.Right: X += 1; break;
            case Direction.Left : X -= 1; break;
            case Direction.Down : Y += 1; break;
            case Direction.Up   : Y -= 1; break;
            default: throw new NotImplementedException();
        }
        return true;
    }

    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}
