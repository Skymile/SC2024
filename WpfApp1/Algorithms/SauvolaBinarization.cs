namespace WpfApp1
{
    public class SauvolaBinarization : NiblackBinarization 
    {
        public override double Formulae(double std, double mean) =>
            std;
    }
}