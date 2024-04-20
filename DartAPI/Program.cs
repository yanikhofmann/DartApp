using Kull.GenericBackend;
using Microsoft.OpenApi.Models;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddMvcCore().AddApiExplorer();
services.AddGenericBackend()
    .ConfigureMiddleware(m =>
    {
        m.AlwaysWrapJson = true;
        m.RequireAuthenticated = true;
    })
    .ConfigureOpenApiGeneration(o =>
    { })
    .AddFileSupport()
    .AddSystemParameters();

// You might have to register your Provider Factory
if (!DbProviderFactories.TryGetFactory("Microsoft.Data.SqlClient", out var _))
    DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);

// IMPORTANT: You have to inject a DbConnection somehow
services.AddTransient(typeof(DbConnection), (s) =>
{
    var conf = s.GetRequiredService<IConfiguration>();
    var constr = conf["ConnectionStrings:DefaultConnection"];
    return new Microsoft.Data.SqlClient.SqlConnection(constr);
});
services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddGenericBackend();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(o =>
    {
        o.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseGenericBackend();

// If needed, Swagger UI, see https://github.com/domaindrivendev/Swashbuckle.AspNetCore
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});