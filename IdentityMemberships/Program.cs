using IdentityMemberships.ConfigurationModels;
using IdentityMemberships.CustomValidations;
using IdentityMemberships.Localizations;
using IdentityMemberships.ServiceCollectionExtensions;
using IdentityMemberships.Services;
using IdentityStructureModel.IdentityDbContexts;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ModelConfigureExtensions(builder.Configuration);
builder.Services.DIConfigurationExtensions();
builder.Services.IdentityConfigurationExtensions();
builder.Services.CookieConfigurationExtensions();
builder.Services.AddStaticRolesConfigurationExtensions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
