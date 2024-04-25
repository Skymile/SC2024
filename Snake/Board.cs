using System.Text;

namespace NetSnake;

public class Board(int width, int height)
{
    public Position? Apple  { get; set; }
    public Snake? Player { get; set; }
    public int    Width  { get; } = width;
    public int    Height { get; } = height;

    public Position? CreateApple()
    {
        ArgumentNullException.ThrowIfNull(Player);

        Position pos;
        do
        {
            pos = new(
                Random.Shared.Next(0, Width),
                Random.Shared.Next(0, Height)
            );
        }
        while (Player.Queue
            .Any(playerPos => playerPos == pos));
        
        return pos;
    }

    public bool IsColliding(Snake snake, Position? apple) =>
        snake.Queue.First?.Value == apple;

    public override string ToString()
    {
        var ceil = new string('-', Width * 2 + 2);

        var sb = new StringBuilder()
            .AppendLine(ceil);

        var first = Player!.Queue.First!.Value;

        for (int y = 0; y < Height; ++y)
        {
            sb.Append('|');
            for (int x = 0; x < Width; x++)
            {
                var xy = new Position(x, y);

                if (first == xy)
                    sb.Append("O ");
                else if (Player!.Queue.Any(p => p == xy))
                    sb.Append("o ");
                else if (Apple?.X == x && Apple?.Y == y)
                    sb.Append("@ ");
                else
                    sb.Append("  ");
            }
            sb.AppendLine("|");
        }

        sb.AppendLine(ceil);

        return sb.ToString();
    }
}
