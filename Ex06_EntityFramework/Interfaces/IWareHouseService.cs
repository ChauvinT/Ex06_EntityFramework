using BO;

namespace Ex06_EntityFramework.Interfaces
{
    public interface IWareHouseService
    {
        public interface IWarehouseService
        {
            // Method to fetch all warehouses
            List<Warehouse> getAllWarehouses();
        }
    }
}
