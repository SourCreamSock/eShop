using Web.Application;
using Web.Application.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<ICatalogService>();
builder.Services.AddControllersWithViews(); 
builder.Services.AddCatalogApiClientServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default","{controller=Catalog}/{action=Index}/{id?}");
app.MapControllers();
app.UseStaticFiles();
//app.UseEndpoints(endpoints => endpoints.MapControllerRoute("default", "{controller/action/id?}"));
app.Run();
