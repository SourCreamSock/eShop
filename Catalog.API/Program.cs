using Catalog.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var conncetionString = builder.Configuration.GetRequiredSection("CatalogConnection");
builder.Services.AddDbContext<CatalogContext>(options => { options.UseSqlServer(); });
builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();   
    await new CatalogContextSeed().SeedAsync(context);
}
app.Run();
    
