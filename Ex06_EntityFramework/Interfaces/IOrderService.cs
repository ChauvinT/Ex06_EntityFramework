namespace Ex06_EntityFramework.Interfaces
{

    public interface IOrderService
    {
        // Method to add new Order
        Orders add(Orders order);

        // Method to delete an order
        void deleteOrder(int orderId);

        // Method to fetch all orders made by a specific customer
        List<Orders> getAllOrdersByCustomer(int customerId);

        // Method to get average order value
        double getAverageOrderValue();

        // Method to get average number article by order
        double getAverageArticlePerOrder();
    }
}
