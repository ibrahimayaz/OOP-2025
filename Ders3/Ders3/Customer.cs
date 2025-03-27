namespace Ders3
{
    public class Customer
    {
        private string name;
        public ICollection<CreditCard> CreditCards { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
