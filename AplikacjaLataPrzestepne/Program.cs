using LeapYearApp.Data;
using Microsoft.AspNetCore.Identity;
using LeapYearApp.Services;
using LeapYearApp.Forms;
using LeapYearApp.Interfaces;
using LeapYearApp.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<helper>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LeapYears")));
builder.Services.AddScoped<LeapYearInterface, LeapYearService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<serv, LeapYearService>();
builder.Services.AddTransient<repo, YearRepo>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<IdentityOptions>(options =>
{
    
    options.Password.RequireUppercase = false;
   
});
builder.Services.AddMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.UseSession();

app.Run();
