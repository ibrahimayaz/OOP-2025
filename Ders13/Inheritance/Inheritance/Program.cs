
Insan i = new();


Insan d = new Doktor();



public interface IYetenek
{
    void Konus();
}

public class Insan:IYetenek
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    protected int Yas { get; set; }

    public void Konus()
    {
        Console.WriteLine("İnsan konuşuyor");
    }
}


public class Doktor : Insan
{
    public string Brans { get; set; }

    public void HastaKontrol()
    {
        
        Console.WriteLine("Hastalar kontrol ediliyor");
    }
}