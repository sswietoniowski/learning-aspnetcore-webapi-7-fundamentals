using CityInfo.API.DataAccess.Data;
using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
using CityInfo.API.DataAccess.Repositories;
using CityInfo.API.DataAccess.Repositories.Interfaces;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

//Serilog
Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Debug()
   .WriteTo.Console()
   .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
   .CreateLogger();

// NLog
//NLog.LogManager.Setup().LoadConfigurationFromAppSettings();

var builder = WebApplication.CreateBuilder(args);

// More info about how we can configure Serilog: https://blog.datalust.co/using-serilog-in-net-6/
// Serilog configuration (with examples) can be found here: https://github.com/serilog/serilog-settings-configuration
// Third-party logging providers: https://docs.microsoft.com/en-us/dotnet/core/extensions/logging-providers#third-party-logging-providers
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Host.UseSerilog((context, loggerConfiguration) =>
//{
//    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
//});
builder.Host.UseSerilog();

// replaced Serilog with NLog using this doc: https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-6
//builder.Host.UseNLog();
// logging NLog logs to the MSSQL Database:
// https://towardsdev.com/writing-logs-into-sql-server-with-nlog-and-net-6-0-fe212f2f6d19


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson(settings =>
    {
        settings.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddDbContext<CityInfoDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// one way of implementing repository & UoW
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// alternative (using something like facade)
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddOptions<MailSettingsConfiguration>()
    .Bind(builder.Configuration.GetSection("MailSettings"))
    .ValidateDataAnnotations(); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// .NET 5.0
app.UseRouting();

app.UseAuthentication(); // authentication middleware before authorization middleware!
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// .NET 6
//app.MapControllers(); // <- contains UseRouting() & UseEndpoints() - not necessarily a good thing

app.Run();
