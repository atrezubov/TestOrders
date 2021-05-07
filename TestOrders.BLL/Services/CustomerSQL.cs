//Services/CustomerSQL.cs.
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
    /// Realisation of ICustomers interface for MS SQL Server.
    /// </summary>
    public class CustomerSQL : ICustomers
    {
        private readonly SQLServerContext _db;

        public CustomerSQL(SQLServerContext db)
        {
            //DI.
            _db = db;
        }

        ///Common function for synchronous and asynchronous methods.
        Customer makeCustomer(CustomerBLL customer)
        {
            Customer dbCustomer = _db.Customers.Where(p => p.Id == customer.Id).FirstOrDefault();

            if (dbCustomer == null)
            {
                return null;
            }

            dbCustomer.Name = customer.Name;
            dbCustomer.Address = customer.Address;
            dbCustomer.Deleted = customer.Deleted;

            return dbCustomer;
        }

        ///Common function for synchronous and asynchronous methods.
        CustomerBLL makeCustomerBLL(Customer customer)
        {
            return new CustomerBLL()
            {
               Id = customer.Id,
               Name = customer.Name,
               Address = customer.Address,
               Deleted = customer.Deleted
            };
        }

        public bool CreateCustomer(CustomerBLL customer)
        {
            Customer newCustomer = new Customer();

            newCustomer.Name = customer.Name;
            newCustomer.Address = customer.Address;
            newCustomer.Deleted = false;

            try
            {
                _db.Customers.Add(newCustomer);
                _db.SaveChanges();
            } catch (Exception e)
            {
               return false;
            }

            return true;
        }

        public async Task<bool> CreateCustomerAsync(CustomerBLL customer)
        {
            Customer newCustomer = new Customer();

            newCustomer.Name = customer.Name;
            newCustomer.Address = customer.Address;
            newCustomer.Deleted = false;

            try
            {
               await _db.Customers.AddAsync(newCustomer);
               await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool EditCustomer(CustomerBLL customer)
        {
            
           try
            {
                _db.Update<Customer>(makeCustomer(customer));
                _db.SaveChanges();
            } catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EditCustomerAsync(CustomerBLL customer)
        {
            try
            {
                _db.Update<Customer>(makeCustomer(customer));
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public CustomerBLL GetCustomer(int customerId)
        {
            return makeCustomerBLL(_db.Customers.Where(p => p.Id == customerId).FirstOrDefault());
        }

        public async Task<CustomerBLL> GetCustomerAsync(int customerId)
        {
            return makeCustomerBLL(await _db.Customers.Where(p => p.Id == customerId).FirstOrDefaultAsync());
        }

        public IEnumerable<CustomerBLL> GetCustomersList()
        {
            IList<CustomerBLL> customersList = new List<CustomerBLL>();

            IList<Customer> customers = _db.Customers.OrderBy(p => p.Id).ToList();

            foreach (Customer cust in customers)
            {
               customersList.Add(makeCustomerBLL(cust));
            }

            return customersList;
        }

        public async Task<IEnumerable<CustomerBLL>> GetCustomersListAsync()
        {
            IList<CustomerBLL> customersList = new List<CustomerBLL>();

            IList<Customer> customers = await _db.Customers.OrderBy(p => p.Id).ToListAsync();

            foreach (Customer cust in customers)
            {
                customersList.Add(makeCustomerBLL(cust));
            }

            return customersList;
        }

       
    }
}
