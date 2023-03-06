using HtmlAgilityPack;
using Crawler;
using Crawler.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<UrlManagerOptions>();
builder.Services.AddSingleton<UrlSet>();
builder.Services.AddScoped<HtmlWeb>();
builder.Services.AddScoped<IBaseUrlConverter, NodeToUrlConverter>();
builder.Services.AddScoped<IUrlValidator, UrlValidator>();
builder.Services.AddScoped<IUrlConverterProvider, UrlConverterProvider>();
builder.Services.AddScoped<UrlFilter>();
builder.Services.AddScoped<UrlManager>();

builder.Services.AddCors(context => context.AddPolicy("MyDefault",builder => {
    builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));


var app = builder.Build();

app.UseCors("MyDefault");

app.UseRouting();
app.MapControllers();

app.Run();