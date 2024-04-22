const int BoardWidth = 12;
const int BoardHeight = 8;

void B()
{
    //
    A();
}

A();

void A()
{
    Console.WriteLine();
}

var snake = new Snake(
    Random.Shared.Next(0, 4),
    Random.Shared.Next(2, BoardHeight)
);
var board = new Board(BoardWidth, BoardHeight)
{
    Player = snake,
};

while (true)
{
    Console.WriteLine(board);
    string c = Console.ReadKey().KeyChar
        .ToString()
        .ToUpper();

    snake.Direction = c switch
    {
        "A" => Direction.Left ,
        "D" => Direction.Right,
        "W" => Direction.Up   ,
        "S" => Direction.Down ,
        _ => Direction.Unknown
    };
    //snake.Direction =
    //    c == "A" ? Direction.Left :
    //    c == "D" ? Direction.Right :
    //    c == "W" ? Direction.Up :
    //    c == "S" ? Direction.Down : Direction.Unknown;

    if (snake.Direction == Direction.Unknown)
        continue;

    snake.Move();
    Console.Clear();
}
