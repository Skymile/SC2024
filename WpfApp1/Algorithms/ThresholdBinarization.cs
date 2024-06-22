using WpfApp1.Algorithms;

namespace WpfApp1
{
    public class ThresholdBinarization : AlgorithmBase
    {
        public int Threshold { get; set; } = 128;

        public override void Apply(Picture pic)
        {
            const int channels = 3;

            for (int i = 0; i < pic.LengthInBytes; i += channels)
            {
                int sum = 0;
                for (int j = 0; j < channels; ++j)
                    sum += pic[i + j];
                sum /= channels;

                byte value = sum > Threshold
                    ? byte.MaxValue
                    : byte.MinValue;

                for (int j = 0; j < channels; j++)
                    pic[i + j] = value;
            }
        }
    }
}