using Ders2;

BankaKullanici b1=new BankaKullanici();

b1.Ad = "İbrahim";
b1.Soyad = "AYAZ";
b1.IBAN = "TR5556898989898984444444";
b1.Bakiye = 10000000;


b1.Havale("TR5556898989898984444441", 10000000);

Console.WriteLine($"Kalan Bakiye: {b1.Bakiye}");


b1.ParaYatir(1520.588);

Console.WriteLine($"Kalan Bakiye: {b1.Bakiye}");