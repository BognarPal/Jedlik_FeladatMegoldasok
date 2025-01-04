using EtelFutar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtelFutar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] dynamic value)
        {
            OrderModel? newItem;
            var menu = ReadMenu();
            try
            {
                newItem = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderModel>(value.ToString());
                if (newItem == null)
                    throw new Exception("Invalid JSON");
                if (string.IsNullOrEmpty(newItem.Email))
                    throw new Exception("Email is required");
                if (string.IsNullOrEmpty(newItem.Name))
                    throw new Exception("Name is required");
                if (string.IsNullOrEmpty(newItem.Address))
                    throw new Exception("Address is required");
                if (newItem.Orders.Count == 0)
                    throw new Exception("Orders is required");
                if (newItem.Orders.Any(o => o.Quantity <= 0))
                    throw new Exception("Quantity must be greater than 0");
                if (newItem.Orders.Any(o => !menu.Any(m => m.Id == o.MenuId) ))
                    throw new Exception("MenuId is invalid");

                List<OrderModel> allOrders = ReadOrders() ?? new List<OrderModel>();
                newItem.Id = allOrders.Count == 0 ? 1 : allOrders.Max(m => m.Id) + 1;

                newItem.Orders.ForEach(o =>
                {
                    o.Price = menu.Single(m => m.Id == o.MenuId).Price;
                });
                newItem.State = "Ordered";

                allOrders.Add(newItem);
                WriteOrders(allOrders);

                return StatusCode(StatusCodes.Status201Created, newItem);
                //Esetleg: return Ok(newItem);  -> viszont ez nem 201-es státuszkódot ad vissza, hanem 200-at
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status400BadRequest, new {error = ex.Message});
                //Rövidebben:
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }

        private List<OrderModel> ReadOrders()
        {
            List<OrderModel>? orders = new List<OrderModel>();
            try
            {
                if (!System.IO.File.Exists(@"Data\orders.json"))
                    return new List<OrderModel>();

                var json = System.IO.File.ReadAllText(@"Data\orders.json");
                if (json.Length < 10)
                    return new List<OrderModel>();
                orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderModel>>(json);
                if (orders == null)
                    throw new Exception("Invalid JSON in orders.json");

                return orders;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void WriteOrders(List<OrderModel> orders)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(orders);
            System.IO.File.WriteAllText(@"Data\orders.json", json);
        }

        private List<MenuModel> ReadMenu()
        {
            List<MenuModel>? menu = new List<MenuModel>();

            try
            {
                var json = System.IO.File.ReadAllText(@"Data\menu.json");
                if (json == null)
                    return new List<MenuModel>();
                menu = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);
                if (menu == null)
                    return new List<MenuModel>();

                return menu;
            }
            catch
            {
                return new List<MenuModel>();
            }
        }


    }
}
