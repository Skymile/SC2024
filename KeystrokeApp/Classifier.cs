using static DistanceMetrics;

public static class Classifier
{
    // Leave-one-out
    public static int KNN(
        SampleSet current,
        SampleSet[] training,
        int k,
        DistanceMetricCallback distance
    ) => (
        from j in (
                from i in training
                let dist = distance(i.DwellTimes, current.DwellTimes)
                orderby dist
                select (Distance: dist, i.Id)
            ).Take(k)
        group j by j.Id into gr
        orderby gr.Count() descending
        select gr.First()
    ).First().Id;
}