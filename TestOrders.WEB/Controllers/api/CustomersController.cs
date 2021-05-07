using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using TestOrders.BLL.Interfaces;
using TestOrders.BLL.Models;
using Newtonsoft.Json;

namespace TestOrders.WEB.Controllers.api
{
    /// <summary>
    /// This controller provides methods for Customers.
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiController] Otherwise, it does not take a parameter in the methods POST and PUT from DevExtreme DataGrid editing form.
    [EnableCors]
    public class CustomersController : ControllerBase
    {
        ICustomers _customers;

        public CustomersController(ICustomers customers)
        {
            _customers = customers; //DI
        }

        /// <summary>
        /// Returns list of customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/getcustomers")]
        public async Task<LoadResult> GetCustomers(DataSourceLoadOptions loadOptions)
        {
            var customersList = await _customers.GetCustomersListAsync();

            return DataSourceLoader.Load(customersList, loadOptions);
        }

        /// <summary>
        /// Create customer method.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost("api/addcustomer")]
        public async Task<IActionResult> CreateCustomer(string values)
        {
            CustomerBLL newCustomer = new CustomerBLL();
            try
            {
                JsonConvert.PopulateObject(values, newCustomer);
            } catch (Exception e)
            {
                return BadRequest("Populating values error.");
            }
           

            if (!TryValidateModel(newCustomer))
            {
                return BadRequest("Validating values error.");
            }

            Boolean result = await _customers.CreateCustomerAsync(newCustomer);

            if (!result)
            {
                return BadRequest("Post values to database error.");
            }

            return Ok();
        }

        /// <summary>
        /// Update customer method.
        /// </summary>
        /// <returns></returns>
        [HttpPut("api/updatecustomer")]
        public async Task<IActionResult> UpdateCustomer(int key, string values)
        {
            CustomerBLL fromdbCustomer = await _customers.GetCustomerAsync(key);
            if (fromdbCustomer == null)
            {
                return BadRequest("Customer not found.");
            }

            try
            {
                JsonConvert.PopulateObject(values, fromdbCustomer);
            }
            catch (Exception e)
            {
                return BadRequest("Populating values error.");
            }

            

            if (!TryValidateModel(fromdbCustomer))
            {
                return BadRequest("Validating values error.");
            }

            Boolean result = await _customers.EditCustomerAsync(fromdbCustomer);

            if (!result)
            {
                return BadRequest("Put values to database error.");
            }

            return Ok();
        }

    }
}
