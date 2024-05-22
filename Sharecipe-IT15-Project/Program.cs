using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Services;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


var connectionString = builder.Configuration.GetConnectionString("SharecipeIdentityDBContextConnection") ?? throw new InvalidOperationException("Connection string 'SharecipeIdentityDBContextConnection' not found.");

builder.Services.AddDbContext<SharecipeIdentityDBContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<SharecipeUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SharecipeIdentityDBContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
});
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<PostService>();
builder.Services.AddTransient<ProfileService>();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProfileService>();


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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapBlazorHub();

app.Run();
