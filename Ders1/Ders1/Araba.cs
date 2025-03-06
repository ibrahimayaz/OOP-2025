namespace Ders1
{
    public class Araba
    {
        /// <summary>
        /// Sınıflar (class) PascalCase yazım kuralına göre yazılmalıdır.
        /// Alanlar(Fields) camelCase yazım kuralına göre yazılmalıdır.
        /// Özellikler(Properties) PascalCase yazım kuralına göre yazılmalıdır.
        /// Metotlar(Methods) PascalCase veya camelCase yazım kuralına göre yazılmalıdıır.
        /// </summary>
        public string ad; //Field
        public string Ad { get; set; } //Özellik
        public string Renk { get; set; }//Özellik
        public int ModelYili { get; set; }//Özellik
        public string Marka { get; set; }//Özellik
        public string YakitTipi { get; set; }//Özellik

        public Araba()
        {
            
        }

        public void gazaBas()
        {
            Console.WriteLine("Araba hızlanıyor");
        }
        public void farAc(bool acikMi)
        {
            if (acikMi) {
                Console.WriteLine($"{Ad} {Marka} isimli arabanın farı açıldı");
            }
            else
            {
                Console.WriteLine($"{Ad} isimli arabanın farı kapatıldı");
            }
        }
    }
}
