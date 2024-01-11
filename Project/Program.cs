using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Project_Data_Access.Data;
using Project_Data_Access.Repository;
using Project_Model;
using Project_utility;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IcategoryRepostry, categoryRepostry>();
builder.Services.AddScoped<IcoverTypeRepostory, CovertypeRepository>();
builder.Services.AddScoped<Iunitofwork, Unitofwork>();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddControllers().ConfigureApiBehaviorOptions(Option =>
{
    Option.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = $"/Identity/Account/Login";
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    option.LogoutPath = $"/Identity/Account/Logout";
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "618900819754817";
    option.AppSecret = "ec51918e52e05d69bcdca3f4ba89c765";
});
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddAuthentication().AddGoogle(option =>
{
    option.ClientId = "763304459898-5ig9p0ep3lbor980osaom92sim5mav0f.apps.googleusercontent.com";
    option.ClientSecret = "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";
});
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["Secretkey"];

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
