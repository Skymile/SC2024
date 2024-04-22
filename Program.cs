using System.Diagnostics;
using System.Text;

static void BB()
{
    var d1 = new TestDispose("d1", 2);
    using var d2 = new TestDispose("d2", 4);
    d1 = null;
}

for (int i = 0; true; i++)
    BB();

;

static void A()
{
    int n = 120;

    string s = "a";
    for (int i = 0; i < 10; i++)
        s += i.ToString();

    // Benchmark.NET
    Stopwatch sw = Stopwatch.StartNew();
    var sb = new StringBuilder();
    for (int i = 0; i < n; i++)
        sb
          .Append(new string(' ', n - i))
          .AppendLine(new string('*', i * 2 + 1));
    Console.WriteLine(sb);
    Console.WriteLine(sw.ElapsedMilliseconds);
    sw.Restart();
    for (int i = 0; i < n; i++)
        Console.WriteLine(string.Concat(
            new string(' ', n - i),
            new string('*', i * 2 + 1)
        ));
    Console.WriteLine(sw.ElapsedMilliseconds);
}

class Reader : IDisposable
{
    ~Reader()
    {
        Dispose();
    }

    public void Dispose()
    {
        //
        GC.SuppressFinalize(this);
    }
}

class TestDispose : IDisposable
{
    public TestDispose(string name, int value)
    {
        this.name = name;
        this.Value = value;
    }

    public int Value { get; private set; }

    ~TestDispose()
    {
        if (name != "d1")
            Console.WriteLine($"Destruktor {name}");
        Value = 0;
    }

    public void Dispose()
    {
        if (name != "d2")
            Console.WriteLine($"Dispose {name}");
        //GC.SuppressFinalize(this);
        Value = 0;
    }

    private readonly string name;
}
