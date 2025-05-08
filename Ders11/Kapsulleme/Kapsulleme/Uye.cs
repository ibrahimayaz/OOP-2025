namespace Kapsulleme
{
    public class Uye
    {
        private string ad;
        private string soyAd;
        private int yas;

        public Uye(string ad, string soyAd, int yas)
        {

            this.ad = ad;
            this.soyAd = soyAd;
            this.yas = yas;
        }
        public int Yas
        {
            get { return yas; }
            set
            {
                this.yas = value;
            }
        }
        public string TamAd
        {
            get
            {
                return $"{ad} {soyAd}";
            }
        }
    }
}
