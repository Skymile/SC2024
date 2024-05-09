using System.Security.Cryptography.X509Certificates;

const string path = @"C:\Keystrokes\";

DistanceMetricCallback distance = DistanceMetrics.Euclidean;
int k = 3;

var all = Parser.ParseDir(path);

foreach ((int Id, Keystroke[] Keystrokes) group in all)
{
	Classifier.KNN(
        new SampleSet(group.Id, group.Keystrokes)
		all .Where(i => i != group)
			.Select(i => new SampleSet(i.Id, i.Keystrokes))
			.ToArray(),
		k,
		distance
	);
}