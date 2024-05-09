public static class Parser
{
    public static IEnumerable<IGrouping<int, Keystroke[]>>
        ParseDir(string directory) =>
        from file in Directory.GetFiles(directory) // Select
        where file.EndsWith(".txt") // Filtrowanie
        let keystrokes = ParseFile(file).ToArray()
        let id = int.Parse(Path.GetFileName(file)[1..3])
        group keystrokes by id into gr
        select gr;

    public static IEnumerable<Keystroke> ParseFile(string path) =>
        ParseLines(File.ReadLines(path));

    public static IEnumerable<Keystroke> ParseLines(
        IEnumerable<string> lines
    ) => 
        from line in lines
        where !string.IsNullOrWhiteSpace(line)
        select Keystroke.FromLine(line);
}