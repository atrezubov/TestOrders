using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOrders.BLL.Models
{
    /// <summary>
    /// Customer model for buisiness layer.
    /// </summary>
    public class CustomerBLL
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Name is required.")]
        [Display(Name = "Customer name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Customer address")]
        public string Address { get; set; }
        [Display(Name = "Deleted")]
        public Boolean Deleted { get; set; }
        public ICollection<OrderBLL> Orders { get; set; }
    }
}
