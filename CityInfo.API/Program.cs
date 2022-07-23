using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

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

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// .NET 6
//app.MapControllers(); // <- contains UseRouting() & UseEndpoints() - not necessarily a good thing

app.Run();
