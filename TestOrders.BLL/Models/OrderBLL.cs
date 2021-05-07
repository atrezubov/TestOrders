using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOrders.BLL.Models
{
    public class OrderBLL
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerBLL Customer { get; set; }
        [Required(ErrorMessage = "Order number is required.")]
        [Display(Name = "Order number")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Order date is required.")]
        [Display(Name = "Order date")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Order amount is required.")]
        [Display(Name = "Amount")]
        public int Amount { get; set; }
        [Display(Name = "Amount")]
        public string Description { get; set; }
        [Display(Name = "Deleted")]
        public Boolean Deleted { get; set; }
    }
}
