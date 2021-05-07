//Entities/Order.cs.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestOrders.DAL.Entities
{
    /// <summary>
    /// Order class.
    /// Model for order in DB.
    /// </summary>
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        // Look for Entities/Customer.cs.
        public Customer Customer { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public Boolean Deleted { get; set; }
    }
}
