using IdentityMemberships.ConfigurationModels;
using IdentityMemberships.CustomValidations;
using IdentityMemberships.Localizations;
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

builder.Services.Configure<EmailConfigurationModel>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
//Identity için olu?turulan token süresi reset password change email
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
{
	opt.TokenLifespan = TimeSpan.FromMinutes(10);
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
		options.User.RequireUniqueEmail = true;
		//lockout-enabled
		options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
		options.Lockout.MaxFailedAccessAttempts = 3;
		options.Lockout.AllowedForNewUsers = true;
	})
	//.AddPasswordValidator<CustomPasswordValidator>()
	//.AddUserValidator<CustomUserValidator>()
	//.AddErrorDescriber<LocalizationIdentityErrorDescription>()
	.AddDefaultTokenProviders()

	.AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
	var cookieBuilder = new CookieBuilder();
	cookieBuilder.Name = "IdentityCookies";

	opt.LoginPath = new PathString("/Account/SignIn");
	opt.Cookie = cookieBuilder;
	opt.ExpireTimeSpan = TimeSpan.FromDays(60);
	opt.SlidingExpiration = true;

});

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
