using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;
using UpakDataAccessLibrary.Repository;
using UpakDataAccessLibrary.Repository.Interfases;

using UpakUtilitiesLibrary.Utility.EmailServices;

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

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(opts =>
{
	opts.IdleTimeout = TimeSpan.FromMinutes(10);
	opts.Cookie.HttpOnly = true;
	opts.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();

var app = builder.Build();
// Configure the HTTP request pipeline.
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
app.Run();
