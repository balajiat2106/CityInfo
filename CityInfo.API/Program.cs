using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using CityInfo.API.Services;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

NLog.LogManager.Setup().LoadConfigurationFromAppSettings();
var logger = NLog.LogManager.GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var ConnString = @"Server=localhost\SQLEXPRESS;Database=CityInfoDB;Trusted_Connection=True;";
    builder.Services.AddDbContext<CityContext>(o => o.UseSqlServer(ConnString));

    builder.Services.AddControllers()
        .AddXmlDataContractSerializerFormatters()
        .AddNewtonsoftJson();

#if DEBUG
    builder.Services.AddTransient<IMailService, MailService>();
#else
    builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

    builder.Services.AddScoped<ICityInfoRepo, CityInfoRepo>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
    }

    app.UseStatusCodePages();
    app.UseRouting();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
