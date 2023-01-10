using back_end.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly back_end.EF.AppContext appContext;

        public ApiController(back_end.EF.AppContext appContext)
        {
            this.appContext = appContext;
        }


        [HttpGet("pizzas")]
        public IActionResult Pizzas()
        {
            return Ok
            (
                appContext.Set<PizzaModel>()
                          .Select(c => new
                          {
                              c.Id,
                              c.Name,
                              c.Price,
                          })
                          .OrderBy(j => j.Name)
            );
        }

        [HttpGet("orders")]
        [SwaggerRequestExample(typeof(string), typeof(string))]
        public IActionResult Routes([FromQuery] string? customerName)
        {
            return Ok
            (
                appContext.Set<OrderModel>()
                          .Include(o => o.Pizza)
                          .Where(o => o.Name.Contains(customerName?? ""))
                          .OrderBy(o => o.Id)
            );
        }

        [HttpPost("order/new")]
        [SwaggerRequestExample(typeof(OrderModel), typeof(OrderModel))]
        public IActionResult NewOrder(dynamic requestOrderModel)
        {
            try
            {
                OrderModel model = System.Text.Json.JsonSerializer.Deserialize<OrderModel>(
                                        requestOrderModel.ToString(),
                                        new System.Text.Json.JsonSerializerOptions()
                                        {
                                            PropertyNameCaseInsensitive = true
                                        });

                var pizza = appContext.Set<PizzaModel>().SingleOrDefault(p => p.Id == model.PizzaId);
                if (pizza == null)
                    throw new MissingFieldException("A pizza kiválasztása kötelezõ");
                if (string.IsNullOrWhiteSpace(model.Name))
                    throw new MissingFieldException("A megrendelõ nevének megadása kötelezõ");
                if (string.IsNullOrWhiteSpace(model.Address))
                    throw new MissingFieldException("A megrendelõ címének megadása kötelezõ");
                if (model.Quantity < 0)
                    throw new MissingFieldException("A mennyiség nem lehet negatív");


                appContext.Set<OrderModel>().Add(model);
                appContext.SaveChanges();
                return StatusCode(201, new
                {
                    id = model.Id,
                    totalPrice = model.Quantity * pizza.Price
                });
            }
            catch (MissingFieldException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest("Hiányos adatok.");
            }
        }

    }
}