using Web.Persistence.Catalog;
using Web.Infrastructure.Mappers;
using Web.Application;

var builder = WebApplication.CreateBuilder(args);

var conncetionString = builder.Configuration.GetConnectionString("CatalogConnection");

builder.Services.AddCatalogContext(conncetionString);
//builder.Services.AddDbContext<CatalogContext>(options =>
//{
//    options.UseSqlServer(conncetionString//options => options.MigrationsAssembly(typeof(Program).Assembly.FullName)
//    );
//    /*builder=> builder.EnableRetryOnFailure(2,TimeSpan.FromSeconds(5),null)*/
//});
builder.Services.AddDefaultMapper();
builder.Services.AddControllers();
builder.Services.AddApplicationServicesForApi();
builder.Services.AddSwaggerGen(options=> {
    
    var xmlFile = "CatalogApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();     

});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.MigrateCatalogDatabase();
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    await new CatalogContextSeed().SeedAsync(context);
}
app.MapControllers();
app.UseStaticFiles();
app.UseSwagger().UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); 
});

app.Run();
