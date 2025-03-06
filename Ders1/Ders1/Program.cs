using Ders1;

Araba araba1 = new();
araba1.Ad = "Fiesta";
araba1.Marka = "Ford";
araba1.Renk = "Beyaz";
araba1.ModelYili = 2015;
araba1.YakitTipi = "Benzin + LPG";

araba1.gazaBas();

araba1.farAc(true);

Araba a2 = new();
a2.Ad = "Toros";
a2.Marka = "Renault";
a2.Renk = "Beyaz";
a2.ModelYili = 1998;
a2.YakitTipi = "Benzin + LPG";

a2.gazaBas();

a2.farAc(true);