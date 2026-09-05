using Web.Persistence.Catalog;
using Web.Application;
using Web.Application.Mappers.AutoMapperProfiles;
using Serilog;
using Catalog.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
try
{

    var conncetionString = builder.Configuration.GetConnectionString("CatalogConnection");

    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddCatalogContext(conncetionString);
    //builder.Services.AddDbContext<CatalogContext>(options =>
    //{
    //    options.UseSqlServer(conncetionString//options => options.MigrationsAssembly(typeof(Program).Assembly.FullName)
    //    );
    //    /*builder=> builder.EnableRetryOnFailure(2,TimeSpan.FromSeconds(5),null)*/
    //});
    builder.Services.AddCatalogServices();
    builder.Services.AddAutoMapper(typeof(DefaultAutoMapperProfile));
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen(options =>
    {

        var xmlFile = "CatalogApi.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        options.EnableAnnotations();

    });

    var app = builder.Build();
    app.UseSerilogRequestLogging();
 
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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Ошибка при запуске приложения");
}
finally{
    Log.CloseAndFlush();
}
