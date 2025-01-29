using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using StripeBlazorApp.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args); // Initialize builder first

// Load Stripe secret key from appsettings.json
var stripeSettings = builder.Configuration.GetSection("Stripe");
StripeConfiguration.ApiKey = stripeSettings["SecretKey"];

Console.WriteLine($"🔹 STRIPE_SECRET_KEY Loaded: {StripeConfiguration.ApiKey}");

// Register services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<StripeService>();
builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:44386/");
});

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
