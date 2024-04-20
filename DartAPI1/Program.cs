using Kull.GenericBackend;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.OpenApi.Models;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dart API", Version = "v1" });
    c.AddGenericBackend();

});

builder.Services.AddSingleton<Kull.GenericBackend.SwaggerGeneration.CodeConvention, DartAPI.GenBackendConvention>();
builder.Services.AddGenericBackend()
    .ConfigureMiddleware(m =>
    { // Set your options
        m.AlwaysWrapJson = false;
#if DEBUG
        m.RequireAuthenticated = false; // for local debug, no user required
#else
        m.RequireAuthenticated = true; 
#endif
    })
    .ConfigureOpenApiGeneration(o =>
    { // Set your options
        o.UseSwagger2 = false;
    })
    .AddFileSupport()
    .AddSystemParameters();
// You might have to register your Provider Factory
if (!DbProviderFactories.TryGetFactory("Microsoft.Data.SqlClient", out var _))
    DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);

builder.Services.AddTransient(typeof(DbConnection), (s) =>
{
    var conf = s.GetRequiredService<IConfiguration>();
    return new Microsoft.Data.SqlClient.SqlConnection(conf["ConnectionStrings:DefaultConnection"]);
});
builder.Services.AddTransient<DartAPI.DataAccess.ErmEntities>();




var app = builder.Build();
var env = app.Environment;

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();


if (env.EnvironmentName == "E2E" || env.EnvironmentName == "Absences")
{
#if DEBUG
    app.Use(async (context, next) =>
    {
        string userName = "KULL\\Forster";
        if (context.Request.Headers.ContainsKey("X-E2E-UserName"))
        {
            userName = context.Request.Headers["X-E2E-UserName"].First();
        }
        if ((context.Request.Host.Host == "::1" || context.Request.Host.Host == "localhost" || context.Request.Host.Host == "127.0.0.1") && context.User?.Identity?.Name == null)
        {
            context.User = new System.Security.Principal.GenericPrincipal(
                       new System.Security.Principal.GenericIdentity(
                           userName, "Windows")
                       , new string[] { });

        }
        await next();
    });
#endif
}

if (env.IsDevelopment() || env.EnvironmentName == "TestProdIssue" || env.EnvironmentName == "FT")
{
#if DEBUG
    app.Use(async (context, next) =>
    {

        if ((context.Request.Host.Host == "::1" || context.Request.Host.Host == "localhost" || context.Request.Host.Host == "127.0.0.1") && context.User?.Identity?.Name == null)
        {
            context.User = new System.Security.Principal.GenericPrincipal(
                       new System.Security.Principal.GenericIdentity(
                           "KULL\\forster", "Windows")
                       , new string[] { });

        }
        await next();
    });
#endif
}
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.UseGenericBackend();
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    endpoints.MapFallback(async context =>
    {
        var environment = context.RequestServices.GetService<IWebHostEnvironment>()!;
        var Request = context.Request;

        var indexContent = System.IO.File.ReadAllText(System.IO.Path.Combine(environment.WebRootPath, "index.html"));
        var baseHrefCode = "<base href=\"/\">";
        var realIndexContent = indexContent.Replace(baseHrefCode, $"<base href=\"{Request.PathBase}/\" >");

        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = 200;
        }
        await context.Response.WriteAsync(realIndexContent);
        await context.Response.CompleteAsync();
    }).RequireAuthorization();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "FT" || app.Environment.EnvironmentName == "E2E")
{
    app.UseSwagger(options =>
    {
        //options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
