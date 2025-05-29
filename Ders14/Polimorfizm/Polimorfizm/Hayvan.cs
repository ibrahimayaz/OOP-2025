namespace Polimorfizm
{
    public class Hayvan
    {
        public string Ad { get; set; }

        public virtual void Konus()
        {
            Console.WriteLine("Hayvan konuşuyor.");
        }
    }
}
