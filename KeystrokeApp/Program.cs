using System.Security.Cryptography.X509Certificates;

const string path = @"C:\Keystrokes\";

DistanceMetricCallback distance = DistanceMetrics.Manhattan;
int k = 3;

var all = Parser.ParseDir(path).ToArray();

int correct = 0;

foreach ((int Id, Keystroke[] Keystrokes) group in all)
{
	int classifiedId = Classifier.KNN(
        new SampleSet(group.Id, group.Keystrokes),
		all .Where(i => i != group)
			.Select(i => new SampleSet(i.Id, i.Keystrokes))
			.ToArray(),
		k,
		distance
	);
	if (group.Id == classifiedId)
		++correct;
    Console.WriteLine($"{group.Id}, {classifiedId}");
}

Console.WriteLine(
	$"Correct: {correct}/{all.Length}," +
	$" % {Math.Round((double)correct / all.Length * 100, 2)}"
);