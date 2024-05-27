using Catalog.API.Infrastructure;
using Catalog.API.Services;

var builder = WebApplication.CreateBuilder(args);

var conncetionString = builder.Configuration.GetConnectionString("CatalogConnection");
builder.Services.AddDbContext<CatalogContext>(options => {
    options.UseSqlServer(conncetionString//options => options.MigrationsAssembly(typeof(Program).Assembly.FullName)
    );
    /*builder=> builder.EnableRetryOnFailure(2,TimeSpan.FromSeconds(5),null)*/ });
builder.Services.AddControllers();
builder.Services.AddSingleton<IPictureHelper, PictureHelper>();

var app = builder.Build();
app.MapControllers();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();   
    await new CatalogContextSeed().SeedAsync(context);
}
app.Run();
    
