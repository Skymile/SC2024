
using System.Text;

public class Board(int width, int height)
{
    public Snake? Player { get; set; }
    public int    Width  { get; } = width;
    public int    Height { get; } = height;

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
            if (Player?.Y == y)
            {
                sb.Append('|')
                  .Append(
                    new string(' ', Player.X * 2))
                  .Append('O')
                  .Append(
                    new string(' ', (Width - Player.X) * 2 - 1))
                  .AppendLine("|");
            }
            else
                sb.AppendLine(row);
        }

        sb.AppendLine(ceil);

        return sb.ToString();
    }
}
