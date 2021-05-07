//Entities/Customer.cs.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestOrders.DAL.Entities
{
    /// <summary>
    /// Customer class.
    /// Model for customers in DB.
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        // 150 enaugh for customer name.
        [MaxLength(150)] 
        public string Name { get; set; }
        public string Address { get; set; }
        public Boolean Deleted { get; set; }
        // Look for Entities/Order.cs.
        public ICollection<Order> Orders { get; set; }
    }
}
