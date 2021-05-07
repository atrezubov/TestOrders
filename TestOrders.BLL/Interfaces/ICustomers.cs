//Interfaces/ICustomers.cs.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOrders.BLL.Models;
using TestOrders.DAL.Entities;

namespace TestOrders.BLL.Interfaces
{
    /// <summary>
    /// Interface for work with customer(s).
    /// </summary>
    public interface ICustomers
    {
        ///Get customers list async.
        public Task<IEnumerable<CustomerBLL>> GetCustomersListAsync();
        ///Get customers list.
        public IEnumerable<CustomerBLL> GetCustomersList();
        ///Get customer async.
        public Task<CustomerBLL> GetCustomerAsync(int customerId);
        ///Get customer.
        public CustomerBLL GetCustomer(int customerId);
        ///Create customer async.
        public Task<Boolean> CreateCustomerAsync(CustomerBLL customer);
        ///Create customer.
        public Boolean CreateCustomer(CustomerBLL customer);
        ///Edit customer async (delete/restore heare too).
        public Task<Boolean> EditCustomerAsync(CustomerBLL customer);
        ///Edit customer (delete/restore here too).
        public Boolean EditCustomer(CustomerBLL customer);

    }
}
