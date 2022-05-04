using Azure.Identity;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddDistributedRedisCache(option =>
{
    option.Configuration = builder.Configuration["redisConnection"];
});

TestConnectedServicesInVS.OTel.SetupOpenTelemetry(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

const string cacheKeyName = "cacheKey";
app.MapGet("/cacheusage", async (IDistributedCache dc) =>
{
    var res = await dc.GetStringAsync(cacheKeyName);
    int nr;

    if (!int.TryParse(res, out nr))
    {
        nr = 0;
    }
    nr++;
    await dc.SetStringAsync(cacheKeyName, nr.ToString());

    return nr;
});

app.MapGet("/directredis", async () =>
{
    var res = await TestConnectedServicesInVS.RedisAccess.Increment();
    return res;
});


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}