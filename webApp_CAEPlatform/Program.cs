using webApp_CAEPlatform.controllers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

HomeController controller = new();

app.UseEndpoints(endpoints =>
{
    
});

app.Run();