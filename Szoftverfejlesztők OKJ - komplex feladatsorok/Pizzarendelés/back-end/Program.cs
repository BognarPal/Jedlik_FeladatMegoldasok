using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<back_end.EF.AppContext>(o => o.UseInMemoryDatabase($"OrderPizza"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(option =>
{
    option.AddPolicy("EnableCORS", builder =>
    {
        builder.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .Build();
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<MySchemaFilter>();

});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseCors("EnableCORS");
app.UseSwagger( c=>
{
    c.RouteTemplate = "docs/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Pizzarendelés API");
    c.RoutePrefix = "docs";
   
});

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<back_end.EF.AppContext>())
    context?.Database.EnsureCreated();

app.UseAuthorization();

app.MapControllers();

app.Run();
