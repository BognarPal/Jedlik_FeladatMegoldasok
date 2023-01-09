using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace back_end.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private readonly AppContext appContext;

    public ApiController(AppContext appContext)
    {
        this.appContext = appContext;
    }

    [HttpGet("cars")]
    public IActionResult Journeys()
    {
        return Ok
        (
            appContext.Set<CarModel>()
                      .Select(c => new 
                      {
                          c.Id,
                          c.LicensePlateNumber,
                          c.FactoryAndModel,
                      })
                      .OrderBy(j => j.FactoryAndModel)
        );
    }

    [HttpGet("routes")]
    [SwaggerRequestExample(typeof(string), typeof(string))]
    public IActionResult Routes([FromQuery]string licensePlateNumber)
    {
        return Ok
        (
            appContext.Set<RouteModel>()
                      .Include(j => j.Car)
                      .Where(j => j.Car.LicensePlateNumber== licensePlateNumber)
                      .OrderBy(j => j.Km)
        );
    }

    [HttpPost("routes/new")]
    [SwaggerRequestExample(typeof(RouteModel), typeof(RouteModel))]
    public IActionResult NewRoute(dynamic requestRouteModel) 
    {
        var cars = appContext.Set<CarModel>().ToList();
        try
        {
            RouteModel model = System.Text.Json.JsonSerializer.Deserialize<RouteModel>(
                                    requestRouteModel.ToString(),
                                    new System.Text.Json.JsonSerializerOptions()
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });

            if (!cars.Any(j => j.Id == model.CarId))
                throw new MissingFieldException("Az autó megadása kötelező");
            if (string.IsNullOrWhiteSpace(model.From)) 
                throw new MissingFieldException("Az indulási helyiség megadás kötelező");
            if (string.IsNullOrWhiteSpace(model.To)) 
                throw new MissingFieldException("A célállomás megadása kötelező");
            if (model.Km < 0) 
                throw new MissingFieldException("A km nem lehet negatív");
            if (string.IsNullOrWhiteSpace(model.DriverName))
                throw new MissingFieldException("A sofőr nevének megadása kötelező");


            appContext.Set<RouteModel>().Add(model);
            appContext.SaveChanges();
            return StatusCode(201, new
            {
                id = model.Id
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
