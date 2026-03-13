using BO;
using Ex06_EntityFramework.Interfaces;
using Ex06_EntityFramework.Models;

namespace Ex06_EntityFramework.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly ApplicationDbContext _context;

        public WareHouseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Warehouse> getAllWarehouse()
        {
            return _context.warehouse.ToList();
        }
    }
}
