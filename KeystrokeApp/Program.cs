const string path = @"C:\Keystrokes\";

var keystrokes = Parser.ParseDir(path).ToArray();

Console.WriteLine("Hello, World!");
