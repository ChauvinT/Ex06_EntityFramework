using Ex06_EntityFramework.Interfaces;
using Ex06_EntityFramework.Models;
using static Ex06_EntityFramework.Interfaces.IWareHouseService;

namespace Ex06_EntityFramework.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Orders add(Orders order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public void deleteOrder(int orderId)
        {
            var order = _context.orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                _context.Remove(order);
                _context.SaveChanges();
            }
        }

        public List<Orders> getAllOrdersByCustomer(int customerId) 
        {
            return _context.orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public double getAverageOrderValue()
        {
            if (!_context.orders.Any())
                return 0;

            return _context.orders.Average(o => o.TotalAmount);
        }

        public double getAverageArticlePerOrder()
        {
            //var totalArticles = _context.orderDetails.Sum(od => od.Quantity);
            //var totalOrders = _context.orders.Count();

            //if (totalOrders == 0)
            //    return 0;

            //return (double)totalArticles / totalOrders;

            return _context.orderDetails
                .GroupBy(od => od.OrderId)
                .Select(g => g.Sum(od => od.Quantity))
                .Average();
        }
    }
}
