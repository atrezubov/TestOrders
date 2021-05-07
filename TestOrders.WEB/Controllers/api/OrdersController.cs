using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOrders.BLL.Interfaces;
using TestOrders.BLL.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Threading;
using Newtonsoft.Json;

namespace TestOrders.WEB.Controllers.api
{
    /// <summary>
    /// This controller provides methods for Orders.
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiController] Otherwise, it does not take a parameter in the methods POST and PUT from DevExtreme DataGrid editing form.
    [EnableCors]
    public class OrdersController : ControllerBase
    {

        IOrders _orders;

        public OrdersController(IOrders orders)
        {
            _orders = orders;
        }

        /// <summary>
        /// Getting list of orders for selected customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet("api/getorders")]
        public async Task<LoadResult> GetOrders(int customerId, DataSourceLoadOptions loadOptions)
        {
            var ordersList = await _orders.GetOrdersListAsync(customerId);

            return DataSourceLoader.Load(ordersList, loadOptions);
        }

        /// <summary>
        /// Creating new order for customer.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost("api/addorder")]
        public async Task<IActionResult> CreateOrder(string values)
        {
            OrderBLL newOrder = new OrderBLL();

            try
            {
                JsonConvert.PopulateObject(values, newOrder);
            } catch (Exception e)
            {
                return BadRequest("Populating values error.");
            }

            if (!TryValidateModel(newOrder))
            {
                return BadRequest("Validating values error.");
            }

            Boolean result = await _orders.CreateOrderAsync(newOrder);

            if (!result)
            {
                return BadRequest("Post values to database error.");
            }

            return Ok();
        }

        /// <summary>
        /// Deleting order from database.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("api/deleteorder")]
        public async Task<IActionResult> DeleteOrder(int key)
        {

            Boolean result = await _orders.DeleteOrderAsync(key);

            if (!result)
            {
                return BadRequest("Error deleting order from database.");
            }

            return Ok();
        }
    }
}
