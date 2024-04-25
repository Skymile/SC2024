namespace NetSnake;

public record struct Position(int X, int Y)
{
    public Position Move(Direction direction) =>
        direction switch
        {
            Direction.Right => this with { X = X + 1 },
            Direction.Left  => this with { X = X - 1 },
            Direction.Down  => this with { Y = Y + 1 },
            Direction.Up    => this with { Y = Y - 1 },

            _ => throw new NotSupportedException()
        };
}

public class Snake
{
    public Snake(int x, int y) =>
        Queue.AddLast(new Position(x, y));

    public Direction Direction { get; set; }

    public bool Move()
    {
        var pos = Queue.First!.Value;
        Queue.AddFirst(pos.Move(Direction));
        Queue.RemoveLast();
        return true;
    }

    public LinkedList<Position> Queue = new();
}
