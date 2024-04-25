using NetSnake;

const int BoardWidth = 12;
const int BoardHeight = 8;

var snake = new Snake(
    Random.Shared.Next(0, 4),
    Random.Shared.Next(2, BoardHeight)
);
var board = new Board(BoardWidth, BoardHeight)
{
    Player = snake,
};

Dictionary<ConsoleKey, Direction> inputToDir = new()
{
    [ConsoleKey.W         ] = Direction.Up   ,
    [ConsoleKey.S         ] = Direction.Down ,
    [ConsoleKey.A         ] = Direction.Left ,
    [ConsoleKey.D         ] = Direction.Right,
    [ConsoleKey.UpArrow   ] = Direction.Up   ,
    [ConsoleKey.DownArrow ] = Direction.Down ,
    [ConsoleKey.LeftArrow ] = Direction.Left ,
    [ConsoleKey.RightArrow] = Direction.Right,
};

while (true)
{
    board.Apple ??= board.CreateApple();

    Console.WriteLine(board);

    if (inputToDir.TryGetValue(
            Console.ReadKey().Key, 
            out Direction direction)
        )
    {
        snake.Direction = direction;
        snake.Move();
    }

    Console.Clear();
}
