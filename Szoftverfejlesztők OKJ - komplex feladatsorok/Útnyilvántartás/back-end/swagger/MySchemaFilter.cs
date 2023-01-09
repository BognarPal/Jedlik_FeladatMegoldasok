using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class MySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.ParameterInfo.Name == "requestRouteModel") {
            schema.Example = new OpenApiObject()
            {
                ["carId"] = new OpenApiInteger(2),
                ["date"] = new OpenApiString($"{DateTime.Today.AddDays(-70):yyyy-MM-dd}"),
                ["from"] = new OpenApiString("Gyõr"),
                ["to"] = new OpenApiString("Budapest"),
                ["km"] = new OpenApiInteger(98554),
                ["driverName"] = new OpenApiString("Gipsz Jakab")
            };
        }
        else if (context.ParameterInfo.Name == "licensePlateNumber")
        {
            schema.Example = new OpenApiString("GDE563");
        }
    }
}