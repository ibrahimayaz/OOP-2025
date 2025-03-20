namespace Ders2
{
    public class BankaKullanici
    {
        private string ad; 
        private string soyad;
        private string iban;
        private double bakiye;

        public string Ad
        {
            get { return ad; }
            set { ad = value; }
        }
        public string Soyad
        {
            get { return soyad; }
            set { soyad = value; }
        }
        public string IBAN
        {
            get { return iban; }
            set { iban = value; }
        }
        public double Bakiye
        {
            get { return bakiye; }
            set { bakiye = value; }
        }

        public bool Havale(string iban, double miktar)
        {
            if (miktar<=Bakiye)
            {
                Console.WriteLine($"{iban} nolu ibana {miktar} TL gönderildi.");
                Bakiye -= miktar; //Bakiye = Bakiye - miktar;
                return true;
            }
            else
            {
                Console.WriteLine($"Yetersiz bakiye");
                return false;
            }
        }

        protected double BakiyeSorgula()
        {
            return Bakiye;
        }

        public void ParaYatir(double miktar)
        {
            Bakiye += miktar;
            Console.WriteLine($"Hesabınıza {miktar} TL para geldi.");
        }
    }
}
