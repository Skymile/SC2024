namespace WpfApp1.Algorithms;

public class K3MThinning : AlgorithmBase
{
    public override void Apply(Picture read)
    {
        read.IsAutoUpdateOff = true;
        int[] ones = new int[read.Width * read.Height];
        int onesCount = 0;

        for (int i = 0; i < read.LengthInBytes; i += read.Channels)
        {
            if (read[i] < 128)
            {
                ones[onesCount++] = i;
                read[i + 0] = One;
                read[i + 1] = One;
                read[i + 2] = One;
            }
            else
            {
                read[i + 0] = Zero;
                read[i + 1] = Zero;
                read[i + 2] = Zero;
            }
        }

        int lastCount = 0;
        int[] borders = new int[onesCount];

        int count;
        while (true)
        {
            count = 0;

            for (int j = 0; j < onesCount; j++)
            {
                int offset = ones[j];
                int sum = ComputeSum(read, offset);

                if (read[offset] == One && A0[sum])
                    borders[count++] = ones[j];
            }

            if (count == lastCount)
                break;
            lastCount = count;

            foreach (var lookup in Lookups)
                for (int j = 0; j < count; j++)
                {
                    int offset = borders[j];
                    int sum = ComputeSum(read, offset);

                    if (read[offset] == One && lookup[sum])
                    {
                        read[offset + 0] = Zero;
                        read[offset + 1] = Zero;
                        read[offset + 2] = Zero;
                    }
                }
        }

        for (int j = 0; j < count; j++)
        {
            int offset = borders[j];
            int sum = ComputeSum(read, offset);

            if (read[offset] == One && A0pix[sum])
            {
                read[offset + 0] = Zero;
                read[offset + 1] = Zero;
                read[offset + 2] = Zero;
            }
        }

        read.Update();
        read.IsAutoUpdateOff = false;
    }

    private bool[] A0 = MakeSet(3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254);

    private bool[][] Lookups = [
        MakeSet(7, 14, 28, 56, 112, 131, 193, 224),
        MakeSet(7, 14, 15, 28, 30, 56, 60, 112, 120, 131, 135, 193, 195, 224, 225, 240),
        MakeSet(7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120, 124, 131, 135, 143, 193, 195, 199, 224, 225, 227, 240, 241, 248),
        MakeSet(7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 193, 195, 199, 207, 224, 225, 227, 231, 240, 241, 243, 248, 249, 252),
        MakeSet(7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 191, 193, 195, 199, 207, 224, 225, 227, 231, 239, 240, 241, 243, 248, 249, 251, 252, 254),
    ];

    private bool[] A0pix = MakeSet(3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254);

    private static bool[] MakeSet(params int[] values)
    {
        var arr = new bool[256];
        for (int i = 0; i < values.Length; i++)
            arr[values[i]] = true;
        return arr;
    }

    private const byte Zero = byte.MaxValue; // White pixels 255
    private const byte One = byte.MinValue; // Black pixels   0


    private int ComputeSum(Picture read, int offset)
    {
        int y = read.Width * read.Channels;
        int x = read.Channels;

        return 0xFF - (
            read[offset - y - x] & 1 << 7 |
            read[offset - x] & 1 << 6 |
            read[offset + y - x] & 1 << 5 |
            read[offset + y] & 1 << 4 |
            read[offset + y + x] & 1 << 3 |
            read[offset + x] & 1 << 2 |
            read[offset - y + x] & 1 << 1 |
            read[offset - y] & 1 << 0
        );
    }
}