using Ders3;

CreditCard ziraatBank = new()
{
    cardNumber = "455445454545458854"
};
CreditCard vakifBank = new()
{
    cardNumber = "887888754545454554"
};

Order hepsiBuradaSiparis1 = new();
Order hepsiBuradaSiparis2 = new();

Customer c1 = new();

c1.CreditCards.Add(vakifBank);
c1.CreditCards.Add(ziraatBank);

c1.Orders.Add(hepsiBuradaSiparis1);
c1.Orders.Add(hepsiBuradaSiparis2);
