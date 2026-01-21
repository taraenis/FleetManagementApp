using FleetManagementApp.Core.Services;
using FleetManagementApp.Core.Services.Api;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<FleetsService>();

builder.Services.Configure<FleetsAPISettings>(
    builder.Configuration.GetSection(FleetsAPISettings.FleetsAPI));
builder.Services.AddHttpClient<FleetsApiService>((serviceProvider, client) => {
    var settings = serviceProvider.GetRequiredService<IOptions<FleetsAPISettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddSingleton<BinpackerService>();
builder.Services.AddSingleton(typeof(IDragDropService<>), typeof(DragDropService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
