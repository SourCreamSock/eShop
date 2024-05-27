using WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<CatalogService>();
builder.Services.AddControllersWithViews(); 

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default","{controller=Catalog}/{action=Index}/{id?}");
app.MapControllers();
app.UseStaticFiles();
//app.UseEndpoints(endpoints => endpoints.MapControllerRoute("default", "{controller/action/id?}"));
app.Run();
