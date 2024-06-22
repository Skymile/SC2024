namespace WpfApp1
{
    public class PhansalkarBinarization : NiblackBinarization 
    {
        public override double Formulae(double std, double mean) =>
            std;
    }
}