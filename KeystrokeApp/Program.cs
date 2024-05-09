const string path = @"C:\Keystrokes\";

(string Name, DistanceMetricCallback Distance)[] distances = [
    ("Manhattan", DistanceMetrics.Manhattan),
	("Chebyshev", DistanceMetrics.Chebyshev),
    ("Euclidean", DistanceMetrics.Euclidean)
];
// BrayCurtis Canberra
var all = Parser.ParseDir(path).ToArray();

for (int k = 1; k < 7; k++)
foreach (var distance in distances)
{
	int correct = 0;
	foreach ((int Id, Keystroke[] Keystrokes) group in all)
	{
		int classifiedId = Classifier.KNN(
	        new SampleSet(group.Id, group.Keystrokes),
			all .Where(i => i != group)
				.Select(i => new SampleSet(i.Id, i.Keystrokes))
				.ToArray(),
			k,
			distance.Distance
		);
		if (group.Id == classifiedId)
			++correct;
	}

	Console.WriteLine(
		$"{k}, {distance.Name}, " + 
		$"Correct: {correct}/{all.Length}," +
		$" % {Math.Round((double)correct / all.Length * 100, 2)}"
	);
}
