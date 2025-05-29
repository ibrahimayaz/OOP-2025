namespace Overloading
{
    public class Matematik
    {
        public int Topla(int x, int y)
        {
            return x + y;
        }
        public double Topla(int x, double y)
        {
            return x + y;
        }
        public double Topla(double x, double y)
        {
            return x + y;
        }

        public double Topla(double x, double y, double z)
        {
            return x + y + z;
        }
    }
}
