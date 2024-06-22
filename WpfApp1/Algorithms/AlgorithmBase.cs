namespace WpfApp1.Algorithms
{
    public abstract class AlgorithmBase
    {
        public virtual void Apply(Picture read, Picture write)
        {
            for (int i = 0; i < read.LengthInBytes; i++)
                write[i] = read[i];
            Apply(write);
        }

        public virtual void Apply(Picture pic)
        {
            var copy = new Picture(pic);
            Apply(pic, copy);
        }
    }
}