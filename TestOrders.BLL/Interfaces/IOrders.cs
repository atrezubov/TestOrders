//Interfaces/IORders.cs.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOrders.BLL.Models;

namespace TestOrders.BLL.Interfaces
{
    /// <summary>
    /// Interface for work with orders.
    /// </summary>
    public interface IOrders
    {
        ///Get list of orders async.
        public Task<ICollection<OrderBLL>> GetOrdersListAsync(int customerID);
        ///Get list of orders.
        public ICollection<OrderBLL> GetOrdersList(int customerID);
        ///Get order async.
        public Task<OrderBLL> GetOrderAsync(int orderID);
        ///Get order.
        public OrderBLL GetOrder(int orderID);
        ///Create order async.
        public Task<Boolean> CreateOrderAsync(OrderBLL order);
        ///Create order.
        public Boolean CreateOrder(OrderBLL order);
        ///Changes Deleted field only.
        public Task<Boolean> UpdateOrderAsync(OrderBLL order);
        ///Changes Deleted field only.
        public Boolean UpdateOrder(OrderBLL order);
        ///Delete order async.
        public Task<Boolean> DeleteOrderAsync(int orderId);
        ///Delete order.
        public Boolean DeleteOrder(int orderId);
    }
}
