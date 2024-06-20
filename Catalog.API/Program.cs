using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.AutoMapperProfiles;
using Catalog.API.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var conncetionString = builder.Configuration.GetConnectionString("CatalogConnection");
builder.Services.AddDbContext<CatalogContext>(options =>
{
    options.UseSqlServer(conncetionString//options => options.MigrationsAssembly(typeof(Program).Assembly.FullName)
    );
    /*builder=> builder.EnableRetryOnFailure(2,TimeSpan.FromSeconds(5),null)*/
});
builder.Services.AddControllers();
builder.Services.AddSingleton<IPictureHelper, PictureHelper>();
builder.Services.AddSwaggerGen(options=> {
    var xmlFile = "CatalogApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(DefaultAutoMapperProfile));

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

