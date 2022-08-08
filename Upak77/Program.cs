using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using Upak77.Data;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary.Utility.EmailServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UpakGkultra2Connextion") ?? throw new InvalidOperationException("Connection string 'MssqlContextConnection' not found.");
builder.Services.AddDbContext<MssqlContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MssqlContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
	options.SignIn.RequireConfirmedAccount = true;
	options.Password.RequiredLength = 5;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireDigit = false;
})
	.AddDefaultTokenProviders()
	.AddDefaultUI()
	.AddEntityFrameworkStores<MssqlContext>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(opts =>
{
	opts.IdleTimeout = TimeSpan.FromMinutes(10);
	opts.Cookie.HttpOnly = true;
	opts.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
