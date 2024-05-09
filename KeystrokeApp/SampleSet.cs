public class SampleSet
{
    public SampleSet(int id, Keystroke[] keystrokes)
    {
        this.Id = id;
        this.Keystrokes = keystrokes;
    }

    public int Id { get; set; }

    public IEnumerable<double> DwellTimes =>
        Keystrokes.Select(i => i.DwellTime);

    public Keystroke[] Keystrokes { get; set; } = [];
}
