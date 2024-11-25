using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.AutoMapperProfiles;
using Catalog.API.Services;


var builder = WebApplication.CreateBuilder(args);

var conncetionString = builder.Configuration.GetConnectionString("CatalogConnection");
builder.Services.AddSingleton<DbContextCustomSettings>(new DbContextCustomSettings { IsUseMigrations = true });
builder.Services.AddDbContext<CatalogContext>(options =>
{
    options.UseSqlServer(conncetionString//options => options.MigrationsAssembly(typeof(Program).Assembly.FullName)
    );
    /*builder=> builder.EnableRetryOnFailure(2,TimeSpan.FromSeconds(5),null)*/
});
builder.Services.AddAutoMapper(typeof(DefaultAutoMapperProfile));
builder.Services.AddControllers();
builder.Services.AddSingleton<IPictureHelper, PictureHelper>();
builder.Services.AddSwaggerGen(options=> {
    
    var xmlFile = "CatalogApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();     

});

var app = builder.Build();
app.MapControllers();
app.UseStaticFiles();
app.UseSwagger().UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); 
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    await new CatalogContextSeed().SeedAsync(context);
}
app.Run();
