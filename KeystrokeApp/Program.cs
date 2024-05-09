using System.Security.Cryptography.X509Certificates;

using static DistanceMetrics;

const string path = @"C:\Keystrokes\";

var keystrokes = Parser.ParseDir(path).ToArray();

Console.WriteLine("Hello, World!");

public static class Classifier
{
    // Leave-one-out
    public static int KNN(
        SampleSet current,
        SampleSet[] training,
        int k,
        DistanceMetricCallback distance
    ) => 0;
}