using BO;
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
                _context.Remove(orderId);
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
            if (!_context.orders.Any())
                return 0;

            return _context.orders.Average(o => o.OrderDetails.Sum(od => od.Quantity));
        }
    }
}
