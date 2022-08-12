using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

using Ultra77Blazor.Areas.Identity;
using Ultra77Blazor.Data;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UpakGkultra2Connextion") ?? throw new InvalidOperationException("Connection string 'MssqlContextConnection' not found.");
builder.Services.AddDbContext<MssqlContext>(options =>
	options.UseSqlServer(connectionString));

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

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddScoped<CartService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSession(opts =>
{
	opts.IdleTimeout = TimeSpan.FromMinutes(10);
	opts.Cookie.HttpOnly = true;
	opts.Cookie.IsEssential = true;
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
