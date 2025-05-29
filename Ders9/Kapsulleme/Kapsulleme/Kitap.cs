namespace Kapsulleme
{
    public class Kitap
    {
        private int sayfaSayisi;

        public int SayfaSayisi
        {
            get { return sayfaSayisi; }
            set
            {
                if (value >= 0)
                {
                    sayfaSayisi = value;
                }
                else
                {
                    Console.WriteLine("SayfaSayısı negatif olamaz !");
                }
            }
        }

        private string kitapAdi;

        public string KitapAdi
        {
            get { return kitapAdi; }
            set { kitapAdi = value; }
        }

        public void KitapGetir()
        {
            Console.WriteLine($"{KitapAdi} - {SayfaSayisi}");
        }
    }
}
