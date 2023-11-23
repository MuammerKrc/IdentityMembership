using IdentityMemberships.CustomValidations;
using IdentityStructureModel.IdentityDbContexts;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(delegate (IdentityOptions options)
	{
		//password
		options.Password.RequireDigit = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 6;
		options.Password.RequiredUniqueChars = 4;
		//user
		options.User.RequireUniqueEmail=true;
	})
	.AddPasswordValidator<CustomPasswordValidator>()
	.AddUserValidator<CustomUserValidator>()
	.AddEntityFrameworkStores<AppIdentityDbContext>()
	.AddDefaultTokenProviders();

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

app.UseAuthorization();


app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
