namespace Ex06_EntityFramework.Models
{
    public class Customers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
