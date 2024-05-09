
public record Keystroke(
        string Key   ,
        int OnPress  ,
        int OnRelease
    )
{
    public double DwellTime => OnRelease;

    // Pure functions
    // 1. No side effects
    // 2. If input doesn't change, output as well
    public static Keystroke FromLine(string line) => 
        FromTokens(line
            .Split(',')
            .Select(str => str.Trim())
            .ToArray()
        );

    public static Keystroke FromTokens(string[] tokens) => new(
        tokens[0],
        int.Parse(tokens[1]),
        int.Parse(tokens[2])
    );
}