//Services/OrderSQL.cs.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOrders.BLL.Interfaces;
using TestOrders.BLL.Models;
using Microsoft.EntityFrameworkCore;
using TestOrders.DAL.EF;
using TestOrders.DAL.Entities;

namespace TestOrders.BLL.Services
{
    /// <summary>
    /// Realisation of IOrders interface for MS SQL Server.
    /// </summary>
    public class OrderSQL : IOrders
    {

        private readonly SQLServerContext _db;

        public OrderSQL(SQLServerContext db)
        {
            _db = db; //DI.
        }

        ///Common function for synchronous and asynchronous methods.
        Order makeOrder(OrderBLL order)
        {

           
            return new Order()
            {
                Amount = order.Amount,
                Date = order.Date,
                Deleted = order.Deleted,
                Description = order.Description,
                CustomerId = order.CustomerId,
                Number = order.Number 
            };
        }

        ///Common function for synchronous and asynchronous methods.
        OrderBLL makeOrderBLL(Order order)
        {
            return new OrderBLL()
            {
                Id = order.Id,
                Amount = order.Amount,
                Date = order.Date,
                Deleted = order.Deleted,
                Description = order.Description,
                CustomerId = order.CustomerId,
                Number = order.Number 
            };
        }

        int getNewOrderNumber()
        {
           return _db.Orders.Max(p => p.Number) + 1;
        }

        Boolean checkIfOrderNumberExists(int orderNumber)
        {
            if (_db.Orders.Where(p => p.Number == orderNumber).Any())
            {
                return true;
            }
                
            return false;
           
        }

        Order getOrderById(int orderId)
        {
            return _db.Orders.Where(p => p.Id == orderId).FirstOrDefault();
        }

        public bool CreateOrder(OrderBLL order)
        {
            
            if (checkIfOrderNumberExists(order.Number))
            {
                order.Number = getNewOrderNumber();
            }

            try
            {
                _db.Orders.Add(makeOrder(order));
                _db.SaveChanges();
            } catch (Exception e)
            {
                return false;
            }

            return true;

        }
        
        public async Task<bool> CreateOrderAsync(OrderBLL order)
        {
            if (checkIfOrderNumberExists(order.Number))
            {
                order.Number = getNewOrderNumber();
            }

            try
            {
               await _db.Orders.AddAsync(makeOrder(order));
               await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public OrderBLL GetOrder(int orderID)
        {
            return makeOrderBLL(_db.Orders.Where(p => p.Id == orderID).FirstOrDefault());

        }

        public async Task<OrderBLL> GetOrderAsync(int orderID)
        {
            return makeOrderBLL(await _db.Orders.Where(p => p.Id == orderID).FirstOrDefaultAsync());
        }

        public ICollection<OrderBLL> GetOrdersList(int customerID)
        {
            IList<OrderBLL> ordersBLL = new List<OrderBLL>();

            IList<Order> orders = _db.Orders.Where(p => p.CustomerId == customerID).ToList();

            foreach(Order order in orders)
            {
                ordersBLL.Add(makeOrderBLL(order));
            }

            return ordersBLL;
        }

        public async Task<ICollection<OrderBLL>> GetOrdersListAsync(int customerID)
        {
            IList<OrderBLL> ordersBLL = new List<OrderBLL>();

            IList<Order> orders = await _db.Orders.Where(p => p.CustomerId == customerID).ToListAsync();

            foreach (Order order in orders)
            {
                ordersBLL.Add(makeOrderBLL(order));
            }

            return ordersBLL;
        }

        ///Changes Deleted field only.
        public bool UpdateOrder(OrderBLL order)
        {

            Order dborder = _db.Orders.Where(p => p.Id == order.Id).FirstOrDefault();
            dborder.Deleted = order.Deleted;

            try
            {
                _db.Orders.Update(dborder);
                _db.SaveChanges();
            } catch(Exception e)
            {
                return false;
            }



            return true;
        }

        ///Changes Deleted field only.
        public async Task<bool> UpdateOrderAsync(OrderBLL order)
        {
            Order dborder = await _db.Orders.Where(p => p.Id == order.Id).FirstOrDefaultAsync();
            dborder.Deleted = order.Deleted;

            try
            {
               _db.Orders.Update(dborder);
               await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }



            return true;
        }

        ///Deleting order by id async.
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            Order dbOrder = getOrderById(orderId);

            try
            {
                _db.Orders.Remove(dbOrder);
                await  _db.SaveChangesAsync();
            } catch (Exception e)
            {
                return false;
            }

            return true;
        }

        ///Deleting order by id.
        public Boolean DeleteOrder(int orderId)
        {
            Order dbOrder = getOrderById(orderId);

            try
            {
                _db.Orders.Remove(dbOrder);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
