namespace Ders2
{
    public class Kitap
    {
        //alanlar 
        private string kitapAdi;
        private string yazarAdi;
        private int basimSayisi;

        //özellikler 
        public int ISBN { get; set; }

        //Metodlar
        public string YazarBilgisiniGetir()
        {
            return yazarAdi;
        }
        public string KitapAdiniGetir() => kitapAdi;
        protected void ISBNDegistir(int isbn)
        {
            ISBN= isbn;
        }
    }
}
