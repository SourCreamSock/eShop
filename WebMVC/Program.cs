using WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddTransient<CatalogService>();
app.UseRouting();
app.UseStaticFiles();
app.UseEndpoints(endpoints => endpoints.MapControllerRoute("default", "{controller/action/id?}"));
app.Run();
