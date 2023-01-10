using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class MySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.ParameterInfo.Name == "requestOrderModel") {
            schema.Example = new OpenApiObject()
            {
                ["pizzaid"] = new OpenApiInteger(2),
                ["name"] = new OpenApiString("Minta Géza"),
                ["address"] = new OpenApiString("Kis köz 2."),
                ["quantity"] = new OpenApiInteger(1),
                ["comment"] = new OpenApiString("Sissenek, mert éhes vagyok")
            };
        }
        else if (context.ParameterInfo.Name == "customerName")
        {
            schema.Example = new OpenApiString("Teszt Elek");
        }
    }
}