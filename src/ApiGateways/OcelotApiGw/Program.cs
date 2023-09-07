using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json",optional:false,reloadOnChange:true);
builder.Services.AddOcelot(builder.Configuration).AddCacheManager(settings => settings.WithDictionaryHandle());
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
