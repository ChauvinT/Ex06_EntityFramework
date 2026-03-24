using Ex06_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex06_EntityFramework
{
    public class Orders
    {
        public int Id { get; set; }


        public string CustomerName { get; set; } = string.Empty;

        public string Email { get; set; }

        public string ShippingAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public double TotalAmount { get; set; }

        public string OrderStatus { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        
        public int WarehouseId { get; internal set; }
        //public Warehouse Warehouse { get; set; } = new Warehouse();

        public int CustomerId { get; set; }

        public Customers Customer { get; set; }

    }
}
