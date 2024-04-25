namespace NetSnake;

[Flags]
public enum Direction
{
    Right = 1,
    Left = 2,
    Up = 4,
    Down = 8,
    Unknown = 16,
}
