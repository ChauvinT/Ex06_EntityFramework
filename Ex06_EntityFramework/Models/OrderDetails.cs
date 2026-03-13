
namespace BO
{
    public class OrderDetails
    {
        public int Id { get; set; }

 
        public int OrderId { get; set; }
        public Orders Order { get; set; }

        public int ArticleId { get; set; }
        public Articles Article { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
