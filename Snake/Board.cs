using System.Text;

namespace NetSnake;

public class Apple(int X, int Y)
{
    public int X { get; } = X;
    public int Y { get; } = Y;
}

public class Board(int width, int height)
{
    public Apple? Apple  { get; set; }
    public Snake? Player { get; set; }
    public int    Width  { get; } = width;
    public int    Height { get; } = height;

    public Apple? CreateApple()
    {
        ArgumentNullException.ThrowIfNull(Player);

        int x;
        int y;
        do
        {
            x = Random.Shared.Next(0, Width);
            y = Random.Shared.Next(0, Height);
        }
        while ((Player.X == x && Player.Y == y) ||
                Player.Queue.Any(i => i.X == x && i.Y == y));

        return new(x, y);
    }

    public override string ToString()
    {
        var row = string.Concat(
            '|', new string(' ', Width * 2), '|'
        );
        var ceil = new string('-', Width * 2 + 2);

        var sb = new StringBuilder()
            .AppendLine(ceil);

        for (int y = 0; y < Height; ++y)
        {
            sb.Append('|');
            for (int x = 0; x < Width; x++)
                if (Player?.X == x && Player.Y == y)
                    sb.Append("O ");
                else if (Apple?.X == x && Apple?.Y == y)
                    sb.Append("@ ");
                else
                    sb.Append("  ");
            sb.AppendLine("|");
        }

        sb.AppendLine(ceil);

        return sb.ToString();
    }
}
