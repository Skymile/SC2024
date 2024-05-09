﻿public static class DistanceMetrics
{
    public static double Euclidean(
        IEnumerable<double> first,
        IEnumerable<double> second
    ) => Math.Sqrt(
        first.Zip(second, (f, s) => f - s).Sum(i => i * i)
    ); 
    // euclidean(A, B) = sqrt( (A_x-B_x)^2 + (A_y + B_y)^2 )

    public static double Manhattan(
        IEnumerable<double> first,
        IEnumerable<double> second
    ) => first.Zip(second, (f, s) => Math.Abs(f - s)).Sum();
    // manhattan(A, B) = |A_x - B_x| + |A_y - B_y|

    public static double Chebyshev(
        IEnumerable<double> first,
        IEnumerable<double> second
    ) => first.Zip(second, (f, s) => Math.Abs(f - s)).Max();
    // chebyshev(A, B) = Max( |A_x - B_x|, |A_y - B_y| )
}

